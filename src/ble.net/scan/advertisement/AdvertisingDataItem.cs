// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.scan.advertisement
{
   /// <summary>
   /// Advertising Data Structure. A single data item in the 31-byte advertising payload
   /// </summary>
   public struct AdvertisingDataItem
   {
      /// <summary>
      /// </summary>
      public AdvertisingDataItem( AdvertisingDataType type, Byte[] data )
      {
         Type = type;
         Data = data;
      }

      /// <summary>
      /// The content of this advertising payload
      /// </summary>
      public Byte[] Data { get; }

      /// <summary>
      /// The type of an entry in the 31-byte advertising PDU payload.
      /// Assigned numbers are used in GAP for inquiry response, EIR data type values, manufacturer-specific data, advertising
      /// data, low energy UUIDs and appearance characteristics, and class of device.
      /// </summary>
      public AdvertisingDataType Type { get; }
   }
}