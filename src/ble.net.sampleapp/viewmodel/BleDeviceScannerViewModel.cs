// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ble.net.sampleapp.util;
using nexus.core.logging;
using nexus.protocols.ble;
using nexus.protocols.ble.scan;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public class BleDeviceScannerViewModel : AbstractScanViewModel
   {
      private readonly Func<BlePeripheralViewModel, Task> m_onSelectDevice;
      private DateTime m_scanStopTime;

      public BleDeviceScannerViewModel( IBluetoothLowEnergyAdapter bleAdapter, IUserDialogs dialogs,
                                        Func<BlePeripheralViewModel, Task> onSelectDevice )
         : base( bleAdapter, dialogs )
      {
         m_onSelectDevice = onSelectDevice;
         FoundDevices = new ObservableCollection<BlePeripheralViewModel>();
         ScanForDevicesCommand =
            new Command( x => { StartScan( x as Double? ?? BleSampleAppUtils.SCAN_SECONDS_DEFAULT ); } );
      }

      public ObservableCollection<BlePeripheralViewModel> FoundDevices { get; }

      public ICommand ScanForDevicesCommand { get; }

      public Int32 ScanTimeRemaining =>
         (Int32)BleSampleAppUtils.ClampSeconds( (m_scanStopTime - DateTime.UtcNow).TotalSeconds );

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
         seconds = BleSampleAppUtils.ClampSeconds( seconds );
         m_scanCancel = new CancellationTokenSource( TimeSpan.FromSeconds( seconds ) );
         m_scanStopTime = DateTime.UtcNow.AddSeconds( seconds );

         Log.Trace( "Beginning device scan. timeout={0} seconds", seconds );

         RaisePropertyChanged( nameof(ScanTimeRemaining) );
         // RaisePropertyChanged of ScanTimeRemaining while scan is running
         Device.StartTimer(
            TimeSpan.FromSeconds( 1 ),
            () =>
            {
               RaisePropertyChanged( nameof(ScanTimeRemaining) );
               return IsScanning;
            } );

         await m_bleAdapter.ScanForBroadcasts(
            // NOTE:
            //
            // You can provide a scan filter to look for particular devices. See Readme.md for more information
            // e.g.:
            //    new ScanFilter().SetAdvertisedManufacturerCompanyId( 224 /*Google*/ ),
            //
            // You can also specify additional scan settings like the amount of power to direct to the Bluetooth antenna:
            // e.g.:
            //    new ScanSettings()
            //    {
            //       Mode = ScanMode.LowPower,
            //       Filter = new ScanFilter().SetAdvertisedManufacturerCompanyId( 224 /*Google*/ )
            //    },
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
                        FoundDevices.Add( new BlePeripheralViewModel( peripheral, m_onSelectDevice ) );
                     }
                  } );
            },
            m_scanCancel.Token );

         IsScanning = false;
      }
   }
}
