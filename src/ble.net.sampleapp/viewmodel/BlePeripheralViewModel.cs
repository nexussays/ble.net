// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.protocols.ble;

namespace ble.net.sampleapp.viewmodel
{
   public class BlePeripheralViewModel
      : BaseViewModel,
        IEquatable<IBlePeripheral>
   {
      private String m_advertisement;

      public BlePeripheralViewModel( IBlePeripheral model )
      {
         Model = model;
         if(Model.Address != null && Model.Address.Length > 0)
         {
            Address = Model.Address.Select( b => b.EncodeToBase16String() ).Join( ":" );
         }
         else
         {
            Address = "";
         }
         Advertisement = Model.Advertisement.ToString();
      }

      public String Address { get; }

      public String AddressAndName => Address + " / " + DeviceName;

      public String AdvertisedServices => Model.Advertisement?.Services.Select(
         x =>
         {
            var name = RegisteredAttributes.GetName( x );
            if(name.IsNullOrEmpty())
            {
               return x.ToString();
            }
            return x.ToString() + " (" + name + ")";
         } ).Join( ", " );

      public String Advertisement
      {
         get { return m_advertisement; }
         private set { Set( ref m_advertisement, value ); }
      }

      public String DeviceName => Model.Advertisement.DeviceName;

      public String Flags => Model.Advertisement?.Flags.ToString( "G" );

      public String Id => Model.DeviceId.ToString();

      public String Manufacturer => Model.Advertisement.ManufacturerSpecificData
                                         .Select( x => BleSampleAppUtils.GetManufacturerName( x.CompanyId ) )
                                         .Join( ", " );

      public String ManufacturerData => Model.Advertisement.ManufacturerSpecificData
                                             .Select(
                                                x => BleSampleAppUtils.GetManufacturerName( x.CompanyId ) + "=0x" +
                                                     x.Data?.ToArray()?.EncodeToBase16String() ).Join( ", " );

      public IBlePeripheral Model { get; private set; }

      public String Name => Model.Advertisement.DeviceName ?? Address;

      public Int32 Rssi => Model.Rssi;

      public String ServiceData => Model.Advertisement?.ServiceData
                                        .Select( x => x.Key + "=0x" + x.Value?.ToArray()?.EncodeToBase16String() )
                                        .Join( ", " );

      public String Signal => Model.Rssi + " / " + Model.Advertisement.TxPowerLevel;

      public Int32 TxPowerLevel => Model.Advertisement.TxPowerLevel;

      public override Boolean Equals( Object other )
      {
         return Model.Equals( other );
      }

      public Boolean Equals( IBlePeripheral other )
      {
         return Model.Equals( other );
      }

      public override Int32 GetHashCode()
      {
         // ReSharper disable once NonReadonlyMemberInGetHashCode
         return Model.GetHashCode();
      }

      public void Update( IBlePeripheral model )
      {
         if(!Equals( Model, model ))
         {
            Model = model;
         }
         Advertisement = Model.Advertisement.ToString();

         RaisePropertyChanged( nameof(Address) );
         RaisePropertyChanged( nameof(AddressAndName) );
         RaisePropertyChanged( nameof(AdvertisedServices) );
         RaisePropertyChanged( nameof(Advertisement) );
         RaisePropertyChanged( nameof(DeviceName) );
         RaisePropertyChanged( nameof(Flags) );
         RaisePropertyChanged( nameof(Manufacturer) );
         RaisePropertyChanged( nameof(ManufacturerData) );
         RaisePropertyChanged( nameof(Model) );
         RaisePropertyChanged( nameof(Name) );
         RaisePropertyChanged( nameof(Rssi) );
         RaisePropertyChanged( nameof(ServiceData) );
         RaisePropertyChanged( nameof(Signal) );
         RaisePropertyChanged( nameof(TxPowerLevel) );
      }
   }
}