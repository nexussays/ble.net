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
using nexus.protocols.ble;
using nexus.protocols.ble.connection;

namespace ble.net.sampleapp.viewmodel
{
   public class BleGattServiceViewModel : BaseViewModel
   {
      private readonly IUserDialogs m_dialogManager;
      private readonly IBleGattServer m_gattServer;
      private Boolean m_isBusy;
      private Guid m_serviceGuid;

      public BleGattServiceViewModel( Guid service, IBleGattServer gattServer, IUserDialogs dialogManager )
      {
         m_serviceGuid = service;
         Characteristic = new ObservableCollection<BleGattCharacteristicViewModel>();
         m_gattServer = gattServer;
         m_dialogManager = dialogManager;
      }

      public ObservableCollection<BleGattCharacteristicViewModel> Characteristic { get; }

      public Guid Guid => m_serviceGuid;

      public String Id => m_serviceGuid.ToString();

      public Boolean IsBusy
      {
         get { return m_isBusy; }
         protected set { Set( ref m_isBusy, value ); }
      }

      public String Name => TiSensorTag.GetName( m_serviceGuid ) ?? Id;

      public String PageTitle => Name;

      public override async void OnAppearing()
      {
         if(IsBusy || Characteristic.Count >= 1)
         {
            return;
         }
         IsBusy = true;
         var services = await m_gattServer.ListServiceCharacteristics( m_serviceGuid );
         var list = services?.ToList();
         if(list != null)
         {
            //Log.Trace( "Discovered chars={0}", list.Select( g => g.ToString() ).Join( "," ) );
            foreach(var c in list)
            {
               Characteristic.Add(
                  new BleGattCharacteristicViewModel( m_serviceGuid, c, m_gattServer, m_dialogManager ) );
            }
         }
         IsBusy = false;
      }
   }
}