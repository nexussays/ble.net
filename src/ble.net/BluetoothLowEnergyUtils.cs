// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core;
using nexus.core.resharper;

namespace nexus.protocols.ble
{
   /// <summary>
   /// Utility methods for BLE
   /// </summary>
   public static class BluetoothLowEnergyUtils
   {
      /// <summary>
      /// The default connection timeout used if no timeout is provided, 5 seconds.
      /// </summary>
      public static readonly TimeSpan DefaultConnectionTimeout = TimeSpan.FromSeconds( 5 );

      /// <summary>
      /// The default time to scan if no timeout is provided, 15 seconds.
      /// </summary>
      public static readonly TimeSpan DefaultScanTimeout = TimeSpan.FromSeconds( 15 );

      /// <summary>
      ///    <c>0000xxxx-0000-1000-8000-00805f9b34fb</c>
      /// </summary>
      private static readonly Byte[] s_adoptedKeyBase =
      {
         0x00,
         0x00,
         0x00,
         0x00,
         0x00,
         0x00,
         0x10,
         0x00,
         0x80,
         0x00,
         0x00,
         0x80,
         0x5f,
         0x9b,
         0x34,
         0xfb
      };

      /// <summary>
      /// Convert a 6-byte (48-bit) MAC address to the trailing 6-bytes of a GUID
      /// </summary>
      /// <param name="address"></param>
      /// <returns></returns>
      public static Guid AddressToGuid( Byte[] address )
      {
         if(address?.Length != 6)
         {
            throw new ArgumentException(
               "Address is invalid. expected=byte[6] got={0}".F(
                  address == null ? null : "byte[" + address.Length + "]" ),
               nameof(address) );
         }
         return new Guid(
            new Byte[]
            {
               0,
               0,
               0,
               0,
               0,
               0,
               0,
               0,
               0,
               0,
               address[0],
               address[1],
               address[2],
               address[3],
               address[4],
               address[5]
            } );
      }

      /// <summary>
      /// Create a <see cref="Guid" /> from a Bluetooth Special Interest Group adopted key
      /// </summary>
      public static Guid CreateGuidFromAdoptedKey( this Int16 adoptedKey )
      {
         // 0000xxxx-0000-1000-8000-00805f9b34fb
         return new Guid( 0x0000FFFF & adoptedKey, 0x0000, 0x1000, 0x80, 0x00, 0x00, 0x80, 0x5f, 0x9b, 0x34, 0xfb );
      }

      /// <summary>
      /// Create a <see cref="Guid" /> from a Bluetooth Special Interest Group adopted key
      /// </summary>
      public static Guid CreateGuidFromAdoptedKey( this UInt16 adoptedKey )
      {
         // 0000xxxx-0000-1000-8000-00805f9b34fb
         return new Guid( 0x0000FFFF & adoptedKey, 0x0000, 0x1000, 0x80, 0x00, 0x00, 0x80, 0x5f, 0x9b, 0x34, 0xfb );
      }

      /// <summary>
      /// Create a <see cref="Guid" /> from a Bluetooth Special Interest Group adopted key
      /// </summary>
      /// <exception cref="ArgumentNullException">If the provided value is null</exception>
      /// <exception cref="ArgumentException">If the provided value is not 4 characters in length</exception>
      /// <exception cref="FormatException">If the provided value cannot be parsed to a Guid</exception>
      public static Guid CreateGuidFromAdoptedKey( [NotNull] this String adoptedKey )
      {
         if(adoptedKey == null)
         {
            throw new ArgumentNullException( nameof(adoptedKey) );
         }
         if(adoptedKey.Length != 4)
         {
            throw new ArgumentException(
               "A Bluetooth adopted key must be a 4-character base16-encoded string",
               nameof(adoptedKey) );
         }
         try
         {
            // 0000xxxx-0000-1000-8000-00805f9b34fb
            return Guid.ParseExact( "0000" + adoptedKey + "-0000-1000-8000-00805f9b34fb", "d" );
         }
         catch(FormatException ex)
         {
            throw new ArgumentException(
               "A Bluetooth adopted key must be a 4-character base16-encoded string",
               nameof(adoptedKey),
               ex );
         }
      }

      /// <summary>
      /// Returns <c>true</c> if <paramref name="id" /> is a Bluetooth SIG reserved GUID
      /// </summary>
      public static Boolean IsReservedKey( this Guid id )
      {
         var bytes = id.ToByteArray();
         bytes[2] = 0;
         bytes[3] = 0;
         return s_adoptedKeyBase.Equals( id );
      }
   }
}