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

namespace ble.net.sampleapp.viewmodel
{
   public class BleGattServiceViewModel : BaseViewModel
   {
      private readonly IBleGattServer m_device;
      private readonly IUserDialogs m_dialogs;
      private Boolean m_isBusy;
      private Guid m_serviceId;

      public BleGattServiceViewModel( Guid service, IBleGattServer device, IUserDialogs dialogs )
      {
         m_serviceId = service;
         Characteristic = new ObservableCollection<BleGattCharacteristicViewModel>();
         m_device = device;
         m_dialogs = dialogs;
      }

      public ObservableCollection<BleGattCharacteristicViewModel> Characteristic { get; }

      public Guid Guid => m_serviceId;

      public String Id => m_serviceId.ToString();

      public Boolean IsBusy
      {
         get { return m_isBusy; }
         protected set { Set( ref m_isBusy, value ); }
      }

      public String Name => null /*KnownAttributes.Get(m_serviceId)?.Description*/?? Id;

      public String PageTitle => Name;

      public override async void OnAppearing()
      {
         if(IsBusy || Characteristic.Count >= 1)
         {
            return;
         }
         IsBusy = true;
         var services = await m_device.ListServiceCharacteristics( m_serviceId );
         var list = services?.ToList();
         if(list != null)
         {
            //Log.Trace( "Discovered chars={0}", list.Select( g => g.ToString() ).Join( "," ) );
            foreach(var c in list)
            {
               Characteristic.Add( new BleGattCharacteristicViewModel( m_serviceId, c, m_device, m_dialogs ) );
            }
         }
         IsBusy = false;
      }
   }
}