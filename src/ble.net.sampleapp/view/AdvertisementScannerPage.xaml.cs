using System;
using ble.net.sampleapp.viewmodel;
using Xamarin.Forms;

namespace ble.net.sampleapp.view
{
   public partial class AdvertisementScannerPage
   {
      public AdvertisementScannerPage( AdvertisementScannerViewModel vm )
      {
         InitializeComponent();
         BindingContext = vm;
      }

      private void ListView_OnItemSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         ((ListView)sender).SelectedItem = null;
      }

      private void Switch_OnToggled( Object sender, ToggledEventArgs e )
      {
         var vm = BindingContext as BleDeviceScannerViewModel;
         if(vm == null)
         {
            return;
         }
         if(e.Value)
         {
            if(vm.EnableAdapterCommand.CanExecute( null ))
            {
               vm.EnableAdapterCommand.Execute( null );
            }
         }
         else if(vm.DisableAdapterCommand.CanExecute( null ))
         {
            vm.DisableAdapterCommand.Execute( null );
         }
      }
   }
}