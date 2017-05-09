// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;
using ble.net.sampleapp.viewmodel;
using nexus.core.logging;
using Xamarin.Forms;

namespace ble.net.sampleapp.view
{
   public partial class BleDeviceScannerPage
   {
      private readonly Func<BlePeripheralViewModel, Task> m_bleDeviceSelected;

      public BleDeviceScannerPage( BleDeviceScannerViewModel model,
                                   Func<BlePeripheralViewModel, Task> bleDeviceSelected )
      {
         m_bleDeviceSelected = bleDeviceSelected;
         InitializeComponent();
         BindingContext = model;
      }

      private void ListView_OnItemSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         Log.Debug( "OnSelected. item={0}", e.SelectedItem );
         if(e.SelectedItem != null)
         {
            m_bleDeviceSelected( (BlePeripheralViewModel)e.SelectedItem );
            ((ListView)sender).SelectedItem = null;
         }
      }

      private void ListView_OnItemTapped( Object sender, ItemTappedEventArgs e )
      {
         Log.Debug( "OnTapped. item={0}", e.Item );
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