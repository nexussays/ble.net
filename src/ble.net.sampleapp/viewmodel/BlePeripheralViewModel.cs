// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.text;
using nexus.protocols.ble.scan;
using nexus.protocols.ble.scan.advertisement;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public class BlePeripheralViewModel
      : BaseViewModel,
        IEquatable<IBlePeripheral>
   {
      private Boolean m_isExpanded;

      public BlePeripheralViewModel( IBlePeripheral model, Func<BlePeripheralViewModel, Task> onSelectDevice )
      {
         Model = model;
         ConnectToDeviceCommand = new Command( async () => { await onSelectDevice( this ); } );
      }

      public String Address => Model.Address != null && Model.Address.Length > 0
         ? Model.Address.Select( b => b.EncodeToBase16String() ).Join( ":" )
         : Id;

      public String AddressAndName => Address + " / " + DeviceName;

      public String AdvertisedServices => Model.Advertisement?.Services.Select(
         x =>
         {
            var name = RegisteredAttributes.GetName( x );
            return name.IsNullOrEmpty()
               ? x.ToString()
               : x.ToString() + " (" + name + ")";
         } ).Join( ", " );

      public String Advertisement => Model.Advertisement.ToString();

      public ICommand ConnectToDeviceCommand { get; }

      public String DeviceName => Model.Advertisement.DeviceName;

      public String Flags => Model.Advertisement?.Flags.ToString( "G" );

      public String Id => Model.DeviceId.ToString();

      public Boolean IsExpanded
      {
         get { return m_isExpanded; }
         set { Set( ref m_isExpanded, value ); }
      }

      public String Manufacturer =>
         Model.Advertisement.ManufacturerSpecificData.Select( x => x.CompanyName() ).Join( ", " );

      public String ManufacturerData => Model.Advertisement.ManufacturerSpecificData
                                             .Select(
                                                x => x.CompanyName() + "=0x" +
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
