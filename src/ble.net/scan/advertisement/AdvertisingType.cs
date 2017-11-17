// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;

namespace nexus.protocols.ble.scan.advertisement
{
   /// <summary>
   /// The type of the Advertising PDU
   /// </summary>
   public enum AdvertisingType
   {
      /// <summary>
      /// Connectable undirected. The advertisement is undirected and indicates that the device is connectable and scannable.
      /// This advertisement type can carry data.
      /// </summary>
      ADV_IND = 0,

      /// <summary>
      /// Connectable directed. The advertisement is directed and indicates that the device is connectable but not scannable.
      /// This advertisement type cannot carry data.
      /// </summary>
      ADV_DIRECT_IND = 1,

      /// <summary>
      /// Non connectable undirected. The advertisement is undirected and indicates that the device is not connectable nor
      /// scannable. This advertisement type can carry data.
      /// </summary>
      ADV_NONCONN_IND = 2,

      /// <summary>
      /// Scannable undirected. The advertisement is undirected and indicates that the device is scannable but not connectable.
      /// This advertisement type can carry data.
      /// </summary>
      ADV_SCAN_IDN = 6,

      SCAN_REQ = 3,

      /// <summary>
      /// This advertisement is a scan response to a scan request issued for a scannable advertisement. This advertisement type
      /// can carry data.
      /// </summary>
      SCAN_RSP = 4,

      /// <summary>
      /// Connection request
      /// </summary>
      CONNECT_REQ = 5
   }

   /// <summary>
   /// Extension methods for <see cref="AdvertisingType" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class AdvertisingTypeExtensions
   {
      /// <summary>
      /// True if this advertising type can contain data: ADV_IND, ADV_NONCONN_IND, and ADV_SCAN_IDN.
      /// </summary>
      public static Boolean CanCarryPayload( this AdvertisingType type )
      {
         return type == AdvertisingType.ADV_IND || type == AdvertisingType.ADV_NONCONN_IND ||
                type == AdvertisingType.ADV_SCAN_IDN;
      }
   }
}