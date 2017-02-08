// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.protocols.ble.advertisement
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

      AdvertisingDataFlags Flags { get; }

      /// <summary>
      /// Manufacturer specific data
      /// </summary>
      IEnumerable<AdvertisingManufacturerData> ManufacturerSpecificData { get; }

      /// <summary>
      /// List of the raw bytes of the advertisement fields, if available.
      /// </summary>
      IEnumerable<AdvertisingDataItem> RawData { get; }

      IEnumerable<KeyValuePair<Guid, Byte[]>> ServiceData { get; }

      /// <summary>
      /// Select services advertised by this peripheral.
      /// </summary>
      IEnumerable<Guid> Services { get; }

      /// <summary>
      /// The transmission power level of the packet in dBm.
      /// </summary>
      Int32 TxPowerLevel { get; }
   }
}