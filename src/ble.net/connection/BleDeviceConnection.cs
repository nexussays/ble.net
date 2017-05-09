// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.connection
{
   /// <summary>
   /// The results of a connection attempt to a remote BLE device
   /// </summary>
   public struct BleDeviceConnection
   {
      /// <summary>
      /// </summary>
      public BleDeviceConnection( ConnectionResult connectionResult, IBleGattServer gattServer )
      {
         ConnectionResult = connectionResult;
         GattServer = gattServer;
      }

      /// <summary>
      /// The result of the connection attempt to a BLE device
      /// </summary>
      public ConnectionResult ConnectionResult { get; }

      /// <summary>
      /// The remote GATT server or null, if the connection was unsuccessful
      /// </summary>
      public IBleGattServer GattServer { get; }
   }
}