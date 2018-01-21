// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using nexus.core;
using nexus.protocols.ble.scan.advertisement;

namespace nexus.protocols.ble.scan
{
   /// <inheritDoc />
   /// <summary>
   /// Base class for platform implementations
   /// </summary>
   [EditorBrowsable(EditorBrowsableState.Never)]
   public abstract class AbstractBlePeripheral : IBlePeripheral
   {
      /// <summary>
      /// The advertisement data for this discovered device
      /// </summary>
      public abstract IBleAdvertisement Advertisement { get; }

      /// <summary>
      /// Constructor for subclasses to call.
      /// </summary>
      /// <remarks>
      /// No point in this being public since it serves no purpose to instantiate outside of retrieving an the actual peripheral
      /// from an adapter -- the interface can always be implemented if needed.
      /// </remarks>
      protected AbstractBlePeripheral( Guid guid, Byte[] address, Boolean? addressIsRandom )
      {
         DeviceId = guid;
         Address = address;
         AddressIsRandom = addressIsRandom;
      }

      /// <inheritdoc />
      public Byte[] Address { get; }

      /// <inheritDoc />
      public Boolean? AddressIsRandom { get; }

      /// <inheritDoc />
      public Guid DeviceId { get; }

      /// <inheritDoc />
      public Int32 Rssi { get; protected set; }

      /// <inheritDoc />
      public override Boolean Equals( Object obj )
      {
         return Equals( obj as IBlePeripheral );
      }

      /// <inheritDoc />
      public Boolean Equals( IBlePeripheral other )
      {
         return !ReferenceEquals( null, other ) && other.DeviceId.Equals( DeviceId );
      }

      /// <inheritDoc />
      public override Int32 GetHashCode()
      {
         return DeviceId.GetHashCode();
      }

      /// <inheritDoc />
      public override String ToString()
      {
         return Advertisement?.DeviceName?.IsNullOrWhiteSpace() != true
            ? "{0} <{1}>".F( Advertisement?.DeviceName, DeviceId )
            : DeviceId.ToString();
      }
   }
}