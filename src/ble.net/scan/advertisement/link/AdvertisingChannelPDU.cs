// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Runtime.InteropServices;

namespace nexus.protocols.ble.scan.advertisement.link
{
   /// <summary>
   /// Structure to represent an advertisement pdu on a <see cref="BlePacket" />
   /// TODO: In-progress
   /// </summary>
   [StructLayout( LayoutKind.Sequential, Pack = 1 )]
   internal struct AdvertisingChannelPDU
   {
      /// <summary>
      /// 2 bytes
      /// </summary>
      public UInt16 header;
      /// <summary>
      /// 6-37 bytes
      /// </summary>
      public Byte[ /*6-37*/] payload;

      /// <summary>
      /// Advertising type (from the header)
      /// </summary>
      public AdvertisingType Type => (AdvertisingType)(header >> 12);

      // 2 bits reserved for future use
      //public Byte Rfu => (Int16)((header >> 10) & 3);

      // 2 bits reserved for future use
      //public Byte Rfu => (Int16)(header  & 3);

      /// <summary>
      /// Payload length (from the header)
      /// </summary>
      public Byte PayloadLength => (Byte)((header >> 2) & 63);

      /// <summary>
      /// Tx add (from the header)
      /// </summary>
      public Boolean TxAdd => ((header >> 9) & 1) == 1;

      /// <summary>
      /// Rx add (from the header)
      /// </summary>
      public Boolean RxAdd => ((header >> 8) & 1) == 1;
   }
}