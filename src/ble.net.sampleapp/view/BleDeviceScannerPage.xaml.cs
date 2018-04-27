// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using ble.net.sampleapp.viewmodel;
using Xamarin.Forms;

namespace ble.net.sampleapp.view
{
   public partial class BleDeviceScannerPage
   {
      public BleDeviceScannerPage( BleDeviceScannerViewModel vm )
      {
         InitializeComponent();
         BindingContext = vm;
      }

      private void ListView_OnItemSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         if(e.SelectedItem != null)
         {
            //((BlePeripheralViewModel)e.SelectedItem).IsExpanded = !((BlePeripheralViewModel)e.SelectedItem).IsExpanded;
            ((ListView)sender).SelectedItem = null;
         }
      }

      private void ListView_OnItemTapped( Object sender, ItemTappedEventArgs e )
      {
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
