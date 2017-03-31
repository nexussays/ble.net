// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
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

      public String Advertisement
      {
         get { return m_advertisement; }
         private set { Set( ref m_advertisement, value ); }
      }

      public String Id => Model.DeviceId.ToString();

      public String Manufacturer
         =>
         Model.Advertisement.ManufacturerSpecificData.Select( x => BleSampleAppUtils.GetManufacturerName( x.CompanyId ) )
              .Join( "," );

      public IBlePeripheral Model { get; }

      public String Name => Model.Advertisement.DeviceName ?? Address;

      public Int32 Rssi => Model.Rssi;

      public String Signal => Model.Rssi + "+" + Model.Advertisement.TxPowerLevel;

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
         return Model.GetHashCode();
      }

      public void Update( IBlePeripheral device )
      {
         Advertisement = Model.Advertisement.ToString();
         //Log.Debug(
         //   "Updating peripheral. name={0} flags={1} services={2} mfg={3} data={4} tx={5} service-data={6}",
         //   device.Advertisement?.DeviceName,
         //   device.Advertisement?.Flags,
         //   device.Advertisement?.Services.Select( x => x.ToString() ).Join( "," ),
         //   device.Advertisement?.ManufacturerSpecificData.Select(
         //            x => x.CompanyId + "|" + x.Data?.ToArray()?.EncodeToBase16String() ).Join( "," ),
         //   device.Advertisement?.RawData.Select( x => x.Type + "|" + x.Data?.ToArray()?.EncodeToBase16String() )
         //         .Join( "," ),
         //   device.Advertisement?.TxPowerLevel,
         //   device.Advertisement?.ServiceData.Select( x => x.Key + "|" + x.Value?.ToArray()?.EncodeToBase16String() )
         //         .Join( "," ) );

         RaisePropertyChanged( nameof( Rssi ) );
         RaisePropertyChanged( nameof( Name ) );
         RaisePropertyChanged( nameof( Signal ) );
         RaisePropertyChanged( nameof( Manufacturer ) );
         RaisePropertyChanged( nameof( Advertisement ) );
      }
   }
}