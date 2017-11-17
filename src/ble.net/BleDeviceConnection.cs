// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;

namespace nexus.protocols.ble
{
   /// <summary>
   /// The results of a connection attempt to a remote BLE device
   /// </summary>
   public struct BleDeviceConnection
   {
      /// <summary>
      /// </summary>
      public BleDeviceConnection( ConnectionResult connectionResult, IBleGattServerConnection gattServer )
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
      public IBleGattServerConnection GattServer { get; }
   }

   /// <summary>
   /// Extension methods for <see cref="BleDeviceConnection" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class BleDeviceConnectionExtensions
   {
      /// <summary>
      /// True if this <see cref="BleDeviceConnection" /> resulted in <see cref="ConnectionResult.Success" />
      /// <remarks>Syntax sugar for <c>device.ConnectionResult == ConnectionResult.Success</c></remarks>
      /// </summary>
      public static Boolean IsSuccessful( this BleDeviceConnection device )
      {
         return device.ConnectionResult == ConnectionResult.Success;
      }
   }
}