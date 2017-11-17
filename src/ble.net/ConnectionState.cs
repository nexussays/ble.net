// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of a remote connection
   /// </summary>
   public enum ConnectionState
   {
      /// <summary>
      /// Disconnected
      /// </summary>
      Disconnected = 0,
      /// <summary>
      /// Disconnecting
      /// </summary>
      Disconnecting = 1,
      /// <summary>
      /// Connecting
      /// </summary>
      Connecting = 2,
      /// <summary>
      /// Connected
      /// </summary>
      Connected = 3
   }
}