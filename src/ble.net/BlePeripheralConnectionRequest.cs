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
   /// The results of an attempted connection to a BLE device. See
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ConnectToDevice(nexus.protocols.ble.scan.IBlePeripheral,System.Threading.CancellationToken,System.IProgress{nexus.protocols.ble.ConnectionProgress})" />
   /// </summary>
   public struct BlePeripheralConnectionRequest
   {
      /// <summary>
      /// </summary>
      public BlePeripheralConnectionRequest( ConnectionResult connectionResult, IBleGattServerConnection gattServer )
      {
         ConnectionResult = connectionResult;
         GattServer = gattServer;
      }

      /// <summary>
      /// The result of the connection attempt to a BLE device
      /// </summary>
      public ConnectionResult ConnectionResult { get; }

      /// <summary>
      /// The remote GATT server or <c>null</c>, if the connection was unsuccessful
      /// </summary>
      public IBleGattServerConnection GattServer { get; }
   }

   /// <summary>
   /// Extension methods for <see cref="BlePeripheralConnectionRequest" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class BleDeviceConnectionExtensions
   {
      /// <summary>
      /// True if this <see cref="BlePeripheralConnectionRequest" /> resulted in <see cref="ConnectionResult.Success" />
      /// <remarks>Syntax sugar for <c>connection.ConnectionResult == ConnectionResult.Success</c></remarks>
      /// </summary>
      public static Boolean IsSuccessful( this BlePeripheralConnectionRequest connection )
      {
         return connection.ConnectionResult == ConnectionResult.Success;
      }
   }
}