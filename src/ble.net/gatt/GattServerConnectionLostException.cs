// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.gatt
{
   /// <summary>
   /// A specific subclass of <see cref="GattException" /> for when a device disconnects or otherwise loses the connection to
   /// facilitate a custom catch clause
   /// </summary>
   public sealed class GattServerConnectionLostException : GattException
   {
      /// <inheritdoc />
      public GattServerConnectionLostException()
         : base( "Connection lost" )
      {
      }
   }
}