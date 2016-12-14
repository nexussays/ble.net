// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.advertisement
{
   /// <summary>
   /// Advertising Data Structure. A single data item in the 31-byte advertising payload
   /// </summary>
   public class AdvertisingDataItem
   {
      public AdvertisingDataItem( AdvertisingDataType type, Byte[] data )
      {
         Type = type;
         Data = data;
      }

      public Byte[] Data { get; }

      public AdvertisingDataType Type { get; }
   }
}