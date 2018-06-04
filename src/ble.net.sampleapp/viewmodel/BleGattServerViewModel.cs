// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;
using nexus.protocols.ble.gatt;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public class BleGattServerViewModel : BaseViewModel
   {
      private const Int32 CONNECTION_TIMEOUT_SECONDS = 15;
      private readonly IBluetoothLowEnergyAdapter m_bleAdapter;
      private readonly IUserDialogs m_dialogManager;
      private String m_connectionState;
      private IBleGattServerConnection m_gattServer;
      private Boolean m_isBusy;
      private BlePeripheralViewModel m_peripheral;

      public BleGattServerViewModel( IUserDialogs dialogsManager, IBluetoothLowEnergyAdapter bleAdapter )
      {
         m_bleAdapter = bleAdapter;
         m_dialogManager = dialogsManager;
         m_connectionState = ConnectionState.Disconnected.ToString();
         Services = new ObservableCollection<BleGattServiceViewModel>();
         DisconnectFromDeviceCommand = new Command( async () => await CloseConnection() );
         ConnectToDeviceCommand = new Command( async () => await OpenConnection() );
      }

      public String Address => m_peripheral?.Address;

      public String AddressAndName =>
         m_peripheral?.AddressAndName; //Address + " / " + (DeviceName ?? "<no device name>");

      public String Connection
      {
         get { return m_connectionState; }
         private set
         {
            if(value != m_connectionState)
            {
               m_connectionState = value;
               RaiseCurrentPropertyChanged();
               RaisePropertyChanged( nameof(IsConnectedOrConnecting) );
            }
         }
      }

      public ICommand ConnectToDeviceCommand { get; }

      public String DeviceName => m_peripheral?.DeviceName;

      public ICommand DisconnectFromDeviceCommand { get; }

      public Boolean IsBusy
      {
         get { return m_isBusy; }
         protected set
         {
            if(value != m_isBusy)
            {
               m_isBusy = value;
               RaiseCurrentPropertyChanged();
               RaisePropertyChanged( nameof(IsConnectedOrConnecting) );
            }
         }
      }

      public Boolean IsConnectedOrConnecting =>
         m_isBusy || m_connectionState != ConnectionState.Disconnected.ToString();

      public String Manufacturer => m_peripheral?.Manufacturer;

      public String Name => m_peripheral?.Name;

      public String PageTitle => "BLE Device GATT Server";

      public Int32? Rssi => m_peripheral?.Rssi;

      public ObservableCollection<BleGattServiceViewModel> Services { get; }

      public async Task OpenConnection()
      {
         // if we're busy or have a valid connection, then no-op
         if(IsBusy || m_gattServer != null && m_gattServer.State != ConnectionState.Disconnected)
         {
            //Log.Debug( "OnAppearing. state={0} isbusy={1}", m_gattServer?.State, IsBusy );
            return;
         }

         await CloseConnection();
         IsBusy = true;

         var connection = await m_bleAdapter.ConnectToDevice(
            device: m_peripheral.Model,
            timeout: TimeSpan.FromSeconds( CONNECTION_TIMEOUT_SECONDS ),
            progress: progress => { Connection = progress.ToString(); } );
         if(connection.IsSuccessful())
         {
            m_gattServer = connection.GattServer;
            Log.Debug( "Connected to device. id={0} status={1}", m_peripheral.Id, m_gattServer.State );

            m_gattServer.Subscribe(
               async c =>
               {
                  if(c == ConnectionState.Disconnected)
                  {
                     m_dialogManager.Toast( "Device disconnected" );
                     await CloseConnection();
                  }

                  Connection = c.ToString();
               } );

            Connection = "Reading Services";
            try
            {
               var services = (await m_gattServer.ListAllServices()).ToList();
               foreach(var serviceId in services)
               {
                  if(Services.Any( viewModel => viewModel.Guid.Equals( serviceId ) ))
                  {
                     continue;
                  }

                  Services.Add( new BleGattServiceViewModel( serviceId, m_gattServer, m_dialogManager ) );
               }

               if(Services.Count == 0)
               {
                  m_dialogManager.Toast( "No services found" );
               }

               Connection = m_gattServer.State.ToString();
            }
            catch(GattException ex)
            {
               Log.Warn( ex );
               m_dialogManager.Toast( ex.Message, TimeSpan.FromSeconds( 3 ) );
            }
         }
         else
         {
            String errorMsg;
            if(connection.ConnectionResult == ConnectionResult.ConnectionAttemptCancelled)
            {
               errorMsg = "Connection attempt cancelled after {0} seconds (see {1})".F(
                  CONNECTION_TIMEOUT_SECONDS,
                  GetType().Name + ".cs" );
            }
            else
            {
               errorMsg = "Error connecting to device: {0}".F( connection.ConnectionResult );
            }

            Log.Info( errorMsg );
            m_dialogManager.Toast( errorMsg, TimeSpan.FromSeconds( 5 ) );
         }

         IsBusy = false;
      }

      public async Task Update( BlePeripheralViewModel peripheral )
      {
         if(m_peripheral != null && !m_peripheral.Model.Equals( peripheral.Model ))
         {
            await CloseConnection();
         }

         m_peripheral = peripheral;
      }

      private async Task CloseConnection()
      {
         IsBusy = true;
         if(m_gattServer != null)
         {
            Log.Trace( "Closing connection to GATT Server. state={0:g}", m_gattServer?.State );
            await m_gattServer.Disconnect();
            m_gattServer = null;
         }

         Services.Clear();
         IsBusy = false;
      }
   }
}