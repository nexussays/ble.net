// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.protocols.ble.advertisement;

namespace nexus.protocols.ble
{
   /// <summary>
   /// Structured data from a BLE perihperal device's advertisement broadcast (and possible also scan response)
   /// </summary>
   public interface IBlePeripheralAdvertisement
   {
      /// <summary>
      /// The name of this device
      /// </summary>
      String DeviceName { get; }

      AdvertisingDataFlags Flags { get; }

      /// <summary>
      /// The manufacturer specific data associated with the manufacturer id.
      /// </summary>
      IEnumerable<KeyValuePair<Int32, Byte[]>> ManufacturerSpecificData { get; }

      /// <summary>
      /// List of the raw bytes of the advertisement fields, if available.
      /// </summary>
      IEnumerable<AdvertisingDataItem> RawData { get; }

      IEnumerable<KeyValuePair<Guid, Byte[]>> ServiceData { get; }

      /// <summary>
      /// Services advertised by this device. Not necessarily an exhaustive list.
      /// </summary>
      IEnumerable<Guid> Services { get; }

      /// <summary>
      /// The transmission power level of the packet in dBm.
      /// </summary>
      Int32 TxPowerLevel { get; }
   }
}