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
   public partial class BleGattServicePage
   {
      public BleGattServicePage( BleGattServiceViewModel model )
      {
         InitializeComponent();
         BindingContext = model;
      }

      private void OnItemSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         ((ListView)sender).SelectedItem = null;
      }
   }
}
