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
   public partial class BleGattServerPage
   {
      private readonly Command m_bleServiceSelectedCommand;

      public BleGattServerPage( BleGattServerViewModel model, Command bleServiceSelectedCommand )
      {
         m_bleServiceSelectedCommand = bleServiceSelectedCommand;
         InitializeComponent();
         BindingContext = model;
      }

      protected override Boolean OnBackButtonPressed()
      {
         ((BleGattServerViewModel)BindingContext).CloseConnection();
         return base.OnBackButtonPressed();
      }

      private void OnServiceSelected( Object sender, SelectedItemChangedEventArgs e )
      {
         if(e.SelectedItem != null)
         {
            if(m_bleServiceSelectedCommand.CanExecute( e.SelectedItem ))
            {
               m_bleServiceSelectedCommand.Execute( e.SelectedItem );
            }
            ((ListView)sender).SelectedItem = null;
         }
      }
   }
}