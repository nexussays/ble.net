// Copyright Malachi Griffie
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
      private readonly Command m_bleDeviceSelectedCommand;

      public BleDeviceScannerPage( BleDeviceScannerViewModel model, Command bleDeviceSelectedCommand )
      {
         m_bleDeviceSelectedCommand = bleDeviceSelectedCommand;
         InitializeComponent();
         BindingContext = model;
      }

      private void OnBleDeviceSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         if(e.SelectedItem != null)
         {
            if(m_bleDeviceSelectedCommand.CanExecute( e.SelectedItem ))
            {
               m_bleDeviceSelectedCommand.Execute( e.SelectedItem );
            }
            ((ListView)sender).SelectedItem = null;
         }
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