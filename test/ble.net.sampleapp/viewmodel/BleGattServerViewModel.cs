// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;

namespace ble.net.sampleapp.viewmodel
{
   public class BleGattServerViewModel : BaseViewModel
   {
      private readonly IBluetoothLowEnergyAdapter m_ble;
      private readonly IUserDialogs m_dialog;
      private readonly BlePeripheralViewModel m_peripheral;
      private IBleGattServer m_device;
      private Boolean m_isBusy;

      public BleGattServerViewModel( BlePeripheralViewModel peripheral, IUserDialogs dialogs,
                                     IBluetoothLowEnergyAdapter ble )
      {
         m_peripheral = peripheral;
         m_ble = ble;
         m_dialog = dialogs;
         Services = new ObservableCollection<BleGattServiceViewModel>();
      }

      public String DeviceConnectionState
         =>
         m_device?.State == ConnectionState.Connected && IsBusy
            ? "Reading Services"
            : (m_device?.State ?? ConnectionState.Disconnected).ToString();

      public Boolean IsBusy
      {
         get { return m_isBusy; }
         protected set { Set( ref m_isBusy, value ); }
      }

      public String PageTitle => m_peripheral.Name;

      public ObservableCollection<BleGattServiceViewModel> Services { get; }

      public void CloseConnection()
      {
         Log.Trace( "Closing connection to GATT Server" );
         m_device?.Dispose();
         Services.Clear();
      }

      public override async void OnAppearing()
      {
         // if we're budy or have a valid connection, then no-op
         if(IsBusy || (m_device != null && m_device.State != ConnectionState.Disconnected))
         {
            return;
         }

         IsBusy = true;
         CloseConnection();
         Log.Debug( "Connecting to device. address={0}", m_peripheral.Address );
         m_device = await m_ble.ConnectToDevice( m_peripheral.Model );
         RaisePropertyChanged( nameof( DeviceConnectionState ) );
         m_device.Subscribe(
            Observer.Create(
               ( ConnectionState c ) =>
               {
                  Log.Info( "Device state changed. address={0} status={1}", m_peripheral.Address, c );
                  RaisePropertyChanged( nameof( DeviceConnectionState ) );
               } ) );
         Log.Debug("Connected to device. address={0} status={1}", m_peripheral.Address, m_device.State);
         if(m_device.State == ConnectionState.Connected || m_device.State == ConnectionState.Connecting)
         {
            var services = (await m_device.ListAllServices()).ToList();
            foreach(var serviceId in services)
            {
               if(Services.Any( viewModel => viewModel.Guid.Equals( serviceId ) ))
               {
                  continue;
               }
               Services.Add( new BleGattServiceViewModel( serviceId, m_device, m_dialog ) );
            }
            if(services.Count == 0)
            {
               m_dialog.Toast( "No services found" );
            }
         }
         else
         {
            Log.Warn(
               "Error connecting to device. address={0} state={1}",
               m_device.Address.EncodeToBase16String(),
               m_device.State );
            m_dialog.Toast( "Error connecting to device" );
         }
         Log.Debug("Read services. address={0} status={1}", m_peripheral.Address, m_device.State);
         IsBusy = false;
         RaisePropertyChanged(nameof(DeviceConnectionState));
      }
   }
}