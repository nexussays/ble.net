using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Acr.UserDialogs;
using nexus.protocols.ble;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public class AdvertisementScannerViewModel : AbstractScanViewModel
   {
      private DateTime m_scanStopTime;

      /// <inheritdoc />
      public AdvertisementScannerViewModel( IBluetoothLowEnergyAdapter bleAdapter, IUserDialogs dialogs )
         : base( bleAdapter, dialogs )
      {
         Advertisements = new ObservableCollection<BlePeripheralViewModel>();
         ScanForDevicesCommand = new Command( x => { StartScan( x as Double? ?? SCAN_SECONDS_DEFAULT ); } );
      }

      public ObservableCollection<BlePeripheralViewModel> Advertisements { get; }

      public ICommand ScanForDevicesCommand { get; }

      public Int32 ScanTimeRemaining => (Int32)ClampSeconds( (m_scanStopTime - DateTime.UtcNow).TotalSeconds );

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
            peripheral =>
            {
               Device.BeginInvokeOnMainThread(
                  () =>
                  {
                     var existing = Advertisements.FirstOrDefault( d => d.Equals( peripheral ) );
                     if(existing != null)
                     {
                        existing.Update( peripheral );
                     }
                     else
                     {
                        Advertisements.Add( new BlePeripheralViewModel( peripheral ) );
                     }
                  } );
            },
            m_scanCancel.Token );

         IsScanning = false;
      }
   }
}