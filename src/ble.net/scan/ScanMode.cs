// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.scan
{
   /// <remarks>Currently this is only used by Android API 21+</remarks>
   /// <summary>
   /// Options to provide to
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ScanForBroadcasts(nexus.protocols.ble.scan.IScanFilter,System.IObserver{nexus.protocols.ble.scan.IBlePeripheral},System.Threading.CancellationToken)" />
   /// to control the strength of the BLE antenna and the amount of power used in the scan.
   /// </summary>
   public enum ScanMode
   {
      /// <summary>
      /// Scan using a balance between power usage and scan latency. Scan results are returned at a rate that provides a good
      /// trade-off between scan frequency and power consumption.
      /// </summary>
      Balanced = 0,

      /// <summary>
      /// Scan using the maximum power usage to the BLE antenna.
      /// <remarks>It's recommended to only use this mode when the application is running in the foreground. </remarks>
      /// </summary>
      HighPower,

      /// <summary>
      /// Scan using less power but with slightly higher latency
      /// </summary>
      LowPower
   }
}