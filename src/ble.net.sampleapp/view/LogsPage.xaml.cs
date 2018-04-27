// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using ble.net.sampleapp.viewmodel;
using Xamarin.Forms;

namespace ble.net.sampleapp.view
{
   public partial class LogsPage
   {
      public LogsPage( LogsViewModel vm )
      {
         BindingContext = vm;
         InitializeComponent();
      }

      /// <inheritdoc />
      protected override void OnBindingContextChanged()
      {
         if(BindingContext != null)
         {
            ((LogsViewModel)BindingContext).PropertyChanged -= LogsPage_PropertyChanged;
         }
         base.OnBindingContextChanged();
         ((LogsViewModel)BindingContext).PropertyChanged += LogsPage_PropertyChanged;
      }

      private void LogsPage_PropertyChanged( Object sender, PropertyChangedEventArgs e )
      {
         scrollView.ScrollToAsync( logsLabel, ScrollToPosition.End, true );
      }
   }
}
