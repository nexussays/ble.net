// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
{
   /// <summary>
   /// An exception at the GATT layer (includes errors as a result of disconnection from a device)
   /// </summary>
   public sealed class GattException : Exception
   {
      public GattException( String message, Exception inner = null )
         : base( message, inner )
      {
      }

      public GattException( String message, Guid service )
         : this( message, service, Guid.Empty, Guid.Empty )
      {
      }

      public GattException( String message, Guid service, Guid characteristic )
         : this( message, service, characteristic, Guid.Empty )
      {
      }

      public GattException( String message, Guid service, Guid characteristic, Guid descriptor )
         : this( message )
      {
         Service = service;
         Characteristic = characteristic;
         Descriptor = descriptor;
      }

      public Guid Characteristic { get; }

      public Guid Descriptor { get; }

      public Guid Service { get; }
   }
}