// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.scan.advertisement
{
   /// <summary>
   /// Data value when type is <see cref="AdvertisingDataType.Flags" />
   /// </summary>
   [Flags]
   public enum AdvertisingDataFlags
   {
      /// <summary>
      /// LE Limited Discoverable Mode.
      /// </summary>
      LimitedDiscoverable = 0b1,

      /// <summary>
      /// LE General Discoverable Mode.
      /// </summary>
      GeneralDiscoverable = 0b10,

      /// <summary>
      /// Basic Rate/Enhanced Data Rate (BR/EDR) not supported.
      /// </summary>
      BluetoothClassicNotSupported = 0b100,

      /// <summary>
      /// Simultaneous Controller for both LE and Basic Rate/Enhanced Data Rate (BR/EDR).
      /// </summary>
      DualModeControllerCapable = 0b1000,

      /// <summary>
      /// Simultaneous Host for both LE and Basic Rate/Enhanced Data Rate (BR/EDR).
      /// </summary>
      DualModeHostCapable = 0b10000
   }
}
