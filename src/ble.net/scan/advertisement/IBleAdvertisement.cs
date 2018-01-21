// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.core.resharper;

namespace nexus.protocols.ble.scan.advertisement
{
   /// <summary>
   /// Structured data from a BLE perihperal device's advertisement broadcast (and possible also scan response)
   /// </summary>
   public interface IBleAdvertisement
   {
      /// <summary>
      /// The name of this device
      /// </summary>
      String DeviceName { get; }

      /// <summary>
      /// The advertisement flags
      /// </summary>
      AdvertisingDataFlags Flags { get; }

      /// <summary>
      /// Manufacturer specific data
      /// </summary>
      [NotNull]
      IEnumerable<AdvertisingManufacturerData> ManufacturerSpecificData { get; }

      /// <summary>
      /// List of the raw bytes of the advertisement fields, if available.
      /// </summary>
      [NotNull]
      IEnumerable<AdvertisingDataItem> RawData { get; }

      /// <summary>
      /// The service data in this advertisement
      /// </summary>
      [NotNull]
      IEnumerable<KeyValuePair<Guid, Byte[]>> ServiceData { get; }

      /// <summary>
      /// Select services advertised by this peripheral.
      /// </summary>
      [NotNull]
      IEnumerable<Guid> Services { get; }

      /// <summary>
      /// The transmission power level of the packet in dBm.
      /// </summary>
      Int32 TxPowerLevel { get; }
   }
}