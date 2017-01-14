// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Acr.UserDialogs;
using ble.net.sampleapp.view;
using ble.net.sampleapp.viewmodel;
using nexus.protocols.ble;
using Xamarin.Forms;

namespace ble.net.sampleapp
{
   public partial class FormsApp
   {
      private readonly NavigationPage m_root;

      public FormsApp( IBluetoothLowEnergyAdapter adapter )
      {
         InitializeComponent();
         var dialogs = UserDialogs.Instance;
         m_root =
            new NavigationPage(
               new BleDeviceScannerPage(
                  model: new BleDeviceScannerViewModel( adapter, dialogs ),
                  bleDeviceSelectedCommand:
                  new Command(
                     async p =>
                     {
                        await
                           m_root.PushAsync(
                              new BleGattServerPage(
                                 model:
                                 new BleGattServerViewModel( (BlePeripheralViewModel)p, UserDialogs.Instance, adapter ),
                                 bleServiceSelectedCommand:
                                 new Command(
                                    async s =>
                                    {
                                       await m_root.PushAsync( new BleGattServicePage( (BleGattServiceViewModel)s ) );
                                    } ) ) );
                     } ) ) );
         MainPage = m_root;
      }
   }
}