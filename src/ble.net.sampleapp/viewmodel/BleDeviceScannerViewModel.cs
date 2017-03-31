// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public class BleDeviceScannerViewModel : BaseViewModel
   {
      private const Int32 SCAN_SECONDS_DEFAULT = 10;
      private const Int32 SCAN_SECONDS_MAX = 30;

      private readonly IBluetoothLowEnergyAdapter m_bleAdapter;
      private readonly IUserDialogs m_dialogs;
      private Boolean m_isScanning;
      private CancellationTokenSource m_scanCancel;
      private DateTime m_scanStopTime;

      public BleDeviceScannerViewModel( IBluetoothLowEnergyAdapter bleAdapter, IUserDialogs dialogs )
      {
         m_bleAdapter = bleAdapter;
         m_dialogs = dialogs;

         FoundDevices = new ObservableCollection<BlePeripheralViewModel>();
         ScanForDevicesCommand = new Command( x => { StartScan( x as Double? ?? SCAN_SECONDS_DEFAULT ); } );
         EnableAdapterCommand = new Command( async () => await ToggleAdapter( true ) );
         DisableAdapterCommand = new Command( async () => await ToggleAdapter( false ) );

         m_bleAdapter.State.Subscribe( state => { RaisePropertyChanged( nameof( IsAdapterEnabled ) ); } );
      }

      public ICommand DisableAdapterCommand { get; private set; }

      public ICommand EnableAdapterCommand { get; private set; }

      public ObservableCollection<BlePeripheralViewModel> FoundDevices { get; }

      public Boolean IsAdapterEnabled
         =>
         m_bleAdapter.State.CurrentState == BleAdapterState.Enabled ||
         m_bleAdapter.State.CurrentState == BleAdapterState.Unknown;

      public Boolean IsScanning
      {
         get { return m_isScanning; }
         protected set { Set( ref m_isScanning, value ); }
      }

      public String PageTitle { get; } = "BLE.net Sample App";

      public ICommand ScanForDevicesCommand { get; }

      public Int32 ScanTimeRemaining => (Int32)ClampSeconds( (m_scanStopTime - DateTime.UtcNow).TotalSeconds );

      public override void OnAppearing()
      {
         RaisePropertyChanged( nameof( IsAdapterEnabled ) );
      }

      public override void OnDisappearing()
      {
         StopScan();
      }

      public void StopScan()
      {
         m_scanCancel?.Cancel();
      }

      private async void StartScan( Double seconds )
      {
         if(IsScanning)
         {
            return;
         }

         if(!IsAdapterEnabled)
         {
            m_dialogs.Toast( "Cannot start scan, Bluetooth is turned off" );
            return;
         }

         StopScan();
         IsScanning = true;

         seconds = ClampSeconds( seconds );
         m_scanCancel = new CancellationTokenSource( TimeSpan.FromSeconds( seconds ) );
         m_scanStopTime = DateTime.UtcNow.AddSeconds( seconds );

         RaisePropertyChanged( nameof( ScanTimeRemaining ) );
         // RaisePropertyChanged of ScanTimeRemaining while scan is running
         Device.StartTimer(
            TimeSpan.FromSeconds( 1 ),
            () =>
            {
               RaisePropertyChanged( nameof( ScanTimeRemaining ) );
               return IsScanning;
            } );

         await
            m_bleAdapter.ScanForBroadcasts(
               //new ScanFilter.Factory { AdvertisedManufacturerCompanyId = (UInt16?)BleSampleAppUtils.BleCompanyId.Google }
               //   .CreateFilter(),
               peripheral =>
               {
                  Device.BeginInvokeOnMainThread(
                     () =>
                     {
                        var existing = FoundDevices.FirstOrDefault( d => d.Equals( peripheral ) );
                        if(existing != null)
                        {
                           existing.Update( peripheral );
                        }
                        else
                        {
                           FoundDevices.Add( new BlePeripheralViewModel( peripheral ) );
                        }
                     } );
               },
               m_scanCancel.Token );

         IsScanning = false;
      }

      private async Task ToggleAdapter( Boolean enable )
      {
         StopScan();
         try
         {
            await (enable ? m_bleAdapter.State.EnableAdapter() : m_bleAdapter.State.DisableAdapter());
         }
         catch(SecurityException ex)
         {
            m_dialogs.Toast( ex.Message );
            Log.Debug( ex, nameof( BleDeviceScannerViewModel ) );
         }
         RaisePropertyChanged( nameof( IsAdapterEnabled ) );
      }

      private static Double ClampSeconds( Double seconds )
      {
         return Math.Max( Math.Min( seconds, SCAN_SECONDS_MAX ), 0 );
      }
   }
}