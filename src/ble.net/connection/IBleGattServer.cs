// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nexus.protocols.ble.connection
{
   /// <summary>
   /// An active connection to a <see cref="IBlePeripheral" />. The GATT Server connection is established when
   /// a Central device successfully establishes a client/server connection to a Peripheral device.
   /// </summary>
   /// <see href="https://www.bluetooth.com/specifications/generic-attributes-overview" />
   public interface IBleGattServer
      : IBlePeripheral,
        IObservable<ConnectionState>,
        IEquatable<IBleGattServer>,
        IDisposable
   {
      /// <summary>
      /// The current state of the connection to the device.
      /// Default: <see cref="ConnectionState.Disconnected" />
      /// </summary>
      ConnectionState State { get; }

      /// <summary>
      /// Be notified of changes to <see cref="State"/>
      /// </summary>
      new IDisposable Subscribe( IObserver<ConnectionState> observer );

      /// <summary>
      /// Enumerate all services on this device
      /// </summary>
      Task<IEnumerable<Guid>> ListAllServices();

      /// <summary>
      /// Enumerate all descriptors of the given characteristic. Descriptors are defined attributes that describe
      /// a characteristic value.
      /// </summary>
      Task<IEnumerable<Guid>> ListCharacteristicDescriptors( Guid service, Guid characteristic );

      /// <summary>
      /// Enumerate all characteristics of the given service. Characteristics are defined attribute types
      /// that contain a single logical value.
      /// </summary>
      Task<IEnumerable<Guid>> ListServiceCharacteristics( Guid service );

      /// <summary>
      /// Listen for NOTIFY events on this characteristic.
      /// </summary>
      IDisposable NotifyCharacteristicValue( Guid service, Guid characteristic, IObserver<Tuple<Guid, Byte[]>> observer );

      /// <summary>
      /// Read the properties of a characteristic
      /// </summary>
      Task<CharacteristicProperty> ReadCharacteristicProperties( Guid service, Guid characteristic );

      /// <summary>
      /// Read the current value of a characteristic
      /// </summary>
      Task<Byte[]> ReadCharacteristicValue( Guid service, Guid characteristic );

      /// <summary>
      /// Read the current value of this descriptor
      /// </summary>
      Task<Byte[]> ReadDescriptorValue( Guid service, Guid characteristic, Guid descriptor );

      /// <summary>
      /// Check if the given service is present on the device.
      /// </summary>
      Task<Boolean> ServiceExists( Guid service );

      /// <summary>
      /// Write to this characteristic's value
      /// </summary>
      Task<Byte[]> WriteCharacteristicValue( Guid service, Guid characteristic, Byte[] data );

      /// <summary>
      /// Write to this characteristic's value using the BLE "write, no response" method
      /// </summary>
      //void WriteCharacteristicValueNoResponse( Guid service, Guid characteristic, Byte[] data );

      /// <summary>
      /// Write the given value to this descriptor
      /// </summary>
      Task<Byte[]> WriteDescriptorValue( Guid service, Guid characteristic, Guid descriptor, Byte[] data );
   }
}