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
      private readonly IBluetoothLowEnergyAdapter m_adapter;
      private readonly IUserDialogs m_dialogs;
      private Boolean m_isScanning;
      private CancellationTokenSource m_scanCancel;

      public BleDeviceScannerViewModel( IBluetoothLowEnergyAdapter adapter, IUserDialogs dialogs )
      {
         m_adapter = adapter;
         m_dialogs = dialogs;
         FoundDevices = new ObservableCollection<BlePeripheralViewModel>();
         ScanForDevicesCommand = new Command( StartScan );
         EnableAdapterCommand = new Command( async () => await ToggleAdapter( true ) );
         DisableAdapterCommand = new Command( async () => await ToggleAdapter( false ) );
         m_adapter.OnStateChanged(
            Observer.Create( ( Boolean state ) => { RaisePropertyChanged( nameof( IsAdapterEnabled ) ); } ) );
      }

      public ICommand DisableAdapterCommand { get; private set; }

      public ICommand EnableAdapterCommand { get; private set; }

      public ObservableCollection<BlePeripheralViewModel> FoundDevices { get; }

      public Boolean IsAdapterEnabled => m_adapter.IsEnabled;

      public Boolean IsScanning
      {
         get { return m_isScanning; }
         protected set { Set( ref m_isScanning, value ); }
      }

      public String PageTitle { get; } = "BLE Devices";

      public ICommand ScanForDevicesCommand { get; }

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

      private async void StartScan()
      {
         Log.Debug( "StartScan. BLE adapter. enabled={0}", m_adapter.IsEnabled );
         if(IsScanning)
         {
            return;
         }
         if(!m_adapter.IsEnabled)
         {
            m_dialogs.Toast( "Cannot start scan, Bluetooth is turned off" );
            return;
         }
         StopScan();
         IsScanning = true;
         m_scanCancel = new CancellationTokenSource( TimeSpan.FromSeconds( 30 ) );
         await m_adapter.ScanForDevices(
            Observer.Create(
               ( IBlePeripheral peripheral ) =>
               {
                  Device.BeginInvokeOnMainThread(
                     () =>
                     {
                        var existing = FoundDevices.FirstOrDefault( d => d.Equals( peripheral ) );
                        if(existing != null)
                        {
                           /*
                           Log.Trace(
                              "Advertisement. rssi={2} name={3} address={0} id={1}",
                              device.Address.EncodeToBase16String(),
                              device.DeviceId,
                              device.Rssi,
                              device.Advertisement.DeviceName );
                           Log.Trace(
                              "   services={0} tx={1} service-data={2}",
                              device.Advertisement.Services.Select( x => x.ToString() ).Join( "," ),
                              device.Advertisement.TxPowerLevel,
                              device.Advertisement.ServiceData.Select( x => x.Key + ":" + x.Value.EncodeToBase16String() ).Join( "," ) );
                           Log.Trace(
                              "   flags={0} mfg-data={1}",
                              device.Advertisement.Flags,
                              device.Advertisement.ManufacturerSpecificData.Select(
                                       x => GetManufacturerName( x.Key ) + ":" + x.Value.EncodeToBase16String() ).Join( "," ) );
                           //*/
                           existing.Update( peripheral );
                        }
                        else
                        {
                           FoundDevices.Add( new BlePeripheralViewModel( peripheral ) );
                        }
                     } );
               } ), m_scanCancel.Token );
         IsScanning = false;
      }

      private async Task ToggleAdapter( Boolean enable )
      {
         StopScan();
         try
         {
            await (enable ? m_adapter.EnableAdapter() : m_adapter.DisableAdapter());
         }
         catch(SecurityException ex)
         {
            m_dialogs.Toast( ex.Message );
            Log.Debug( ex, nameof( BleDeviceScannerViewModel ) );
         }
         RaisePropertyChanged( nameof( IsAdapterEnabled ) );
      }
   }
}