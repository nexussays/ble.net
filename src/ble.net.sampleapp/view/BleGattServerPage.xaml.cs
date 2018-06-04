// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;
using ble.net.sampleapp.viewmodel;
using Xamarin.Forms;

namespace ble.net.sampleapp.view
{
   public partial class BleGattServerPage
   {
      private readonly Func<BleGattServiceViewModel, Task> m_bleServiceSelected;

      public BleGattServerPage( BleGattServerViewModel model, Func<BleGattServiceViewModel, Task> bleServiceSelected )
      {
         m_bleServiceSelected = bleServiceSelected;
         InitializeComponent();
         BindingContext = model;
      }

      protected override Boolean OnBackButtonPressed()
      {
         ((BleGattServerViewModel)BindingContext).DisconnectFromDeviceCommand.Execute( null );
         return base.OnBackButtonPressed();
      }

      private void OnServiceSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         if(e.SelectedItem != null)
         {
            m_bleServiceSelected( (BleGattServiceViewModel)e.SelectedItem );
            ((ListView)sender).SelectedItem = null;
         }
      }
   }
}