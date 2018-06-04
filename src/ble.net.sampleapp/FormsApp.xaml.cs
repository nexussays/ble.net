// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Reflection;
using Acr.UserDialogs;
using ble.net.sampleapp.view;
using ble.net.sampleapp.viewmodel;
using nexus.core.logging;
using nexus.protocols.ble;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
#if RELEASE
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
#endif

namespace ble.net.sampleapp
{
   public partial class FormsApp
   {
      private readonly IUserDialogs m_dialogs;
      private readonly NavigationPage m_rootPage;

      public FormsApp( IBluetoothLowEnergyAdapter adapter, IUserDialogs dialogs )
      {
         InitializeComponent();

         m_dialogs = dialogs;
         var logsVm = new LogsViewModel();
         SystemLog.Instance.AddSink( logsVm );

         var bleAssembly = adapter.GetType().GetTypeInfo().Assembly.GetName();
         Log.Info( bleAssembly.Name + "@" + bleAssembly.Version );

         var bleGattServerViewModel = new BleGattServerViewModel( dialogs, adapter );
         var bleScanViewModel = new BleDeviceScannerViewModel(
            bleAdapter: adapter,
            dialogs: dialogs,
            onSelectDevice: async p =>
            {
               await bleGattServerViewModel.Update( p );
               await m_rootPage.PushAsync(
                  new BleGattServerPage(
                     model: bleGattServerViewModel,
                     bleServiceSelected: async s => { await m_rootPage.PushAsync( new BleGattServicePage( s ) ); } ) );
               await bleGattServerViewModel.OpenConnection();
            } );

         m_rootPage = new NavigationPage(
            new TabbedPage
            {
               Title = "BLE.net Sample App",
               Children = {new BleDeviceScannerPage( bleScanViewModel ), new LogsPage( logsVm )}
            } );

         MainPage = m_rootPage;
      }

      /// <inheritdoc />
      protected override void OnStart()
      {
         base.OnStart();
         if(Device.RuntimePlatform == Device.Windows)
         {
            Device.StartTimer(
               TimeSpan.FromSeconds( 3 ),
               () =>
               {
                  m_dialogs.Alert(
                     "The UWP API can listen for advertisements but is not yet able to connect to devices.",
                     "Quick Note",
                     "Aww, ok" );
                  return false;
               } );
         }
      }
   }
}
