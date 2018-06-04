// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.gatt
{
   /// <summary>
   /// An exception at the GATT layer (includes errors as a result of disconnection from a device, <see cref="GattServerConnectionLostException"/>)
   /// </summary>
   public class GattException : Exception
   {
      /// <inheritdoc />
      public GattException( String message, Exception inner = null )
         : base( message, inner )
      {
      }

      /// <inheritdoc />
      public GattException( String message, Guid service )
         : this( message, service, Guid.Empty, Guid.Empty )
      {
      }

      /// <inheritdoc />
      public GattException( String message, Guid service, Guid characteristic )
         : this( message, service, characteristic, Guid.Empty )
      {
      }

      /// <inheritdoc />
      public GattException( String message, Guid service, Guid characteristic, Guid descriptor )
         : this( message )
      {
         Service = service;
         Characteristic = characteristic;
         Descriptor = descriptor;
      }

      /// <summary>
      /// The characteristic involved in the exception, if there was one.
      /// </summary>
      public Guid Characteristic { get; }

      /// <summary>
      /// The descriptor involved in the exception, if there was one.
      /// </summary>
      public Guid Descriptor { get; }

      /// <summary>
      /// The service involved in the exception, if there was one.
      /// </summary>
      public Guid Service { get; }

      /// <inheritdoc />
      public override String ToString()
      {
         return base.ToString() + (!Service.Equals( Guid.Empty ) ? $" service={Service}" : "") +
                (!Characteristic.Equals( Guid.Empty ) ? $" characteristic={Characteristic}" : "") +
                (!Descriptor.Equals( Guid.Empty ) ? $" descriptor={Descriptor}" : "");
      }
   }
}