// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// TODO: In-progress
   /// </summary>
   internal enum ScanMode
   {
      /// <summary>
      /// Use a balance between power usage and scan latency
      /// </summary>
      Balanced = 0,
      /// <summary>
      /// Use more power to the BLE antenna to achieve lower latency
      /// </summary>
      HighPowerLowLatency,
      /// <summary>
      /// Use less power but slightly higher latency
      /// </summary>
      LowPowerHighLatency
   }
}