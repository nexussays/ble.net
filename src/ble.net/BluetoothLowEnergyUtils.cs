// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using nexus.core;
using nexus.protocols.ble.advertisement;
using nexus.protocols.ble.connection;

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
      /// Return true if <see cref="CharacteristicProperty.Indicate" /> is set on this characteristic
      /// </summary>
      public static Boolean CanIndicate( this CharacteristicProperty properties )
      {
         return (properties & CharacteristicProperty.Indicate) != 0;
      }

      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Notify" /> is set on this characteristic
      /// </summary>
      public static Boolean CanNotify( this CharacteristicProperty properties )
      {
         return (properties & CharacteristicProperty.Notify) != 0;
      }

      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Read" /> is set on this characteristic
      /// </summary>
      public static Boolean CanRead( this CharacteristicProperty properties )
      {
         return (properties & CharacteristicProperty.Read) != 0;
      }

      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Write" /> or <see cref="CharacteristicProperty.WriteNoResponse" /> are
      /// set on this characteristic
      /// </summary>
      public static Boolean CanWrite( this CharacteristicProperty properties )
      {
         return (properties & (CharacteristicProperty.Write | CharacteristicProperty.WriteNoResponse)) != 0;
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
      public static Guid CreateGuidFromAdoptedKey( this String adoptedKey )
      {
         Contract.Requires<ArgumentNullException>( adoptedKey != null );
         Debug.Assert( adoptedKey != null, "adoptedKey != null" );
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

      /// <summary>
      /// True if this <see cref="BleDeviceConnection" /> resulted in <see cref="ConnectionResult.Success" />
      /// </summary>
      public static Boolean IsSuccessful( this BleDeviceConnection device )
      {
         return device.ConnectionResult == ConnectionResult.Success;
      }

      /// <summary>
      /// Listen for NOTIFY events on this characteristic.
      /// </summary>
      public static IDisposable NotifyCharacteristicValue( this IBleGattServer server, Guid service,
                                                           Guid characteristic, Action<Tuple<Guid, Byte[]>> onNotify,
                                                           Action<Exception> onError = null )
      {
         Contract.Requires<ArgumentNullException>( server != null );
         // ReSharper disable once PossibleNullReferenceException
         return server.NotifyCharacteristicValue( service, characteristic, Observer.Create( onNotify, null, onError ) );
      }

      /// <summary>
      /// Listen for NOTIFY events on this characteristic.
      /// </summary>
      public static IDisposable NotifyCharacteristicValue( this IBleGattServer server, Guid service,
                                                           Guid characteristic, Action<Byte[]> onNotify,
                                                           Action<Exception> onError = null )
      {
         Contract.Requires<ArgumentNullException>( server != null );
         // ReSharper disable once PossibleNullReferenceException
         return server.NotifyCharacteristicValue(
            service,
            characteristic,
            Observer.Create( ( Tuple<Guid, Byte[]> tuple ) => onNotify( tuple.Item2 ), null, onError ) );
      }

      /// <summary>
      /// Parse <c>advD</c> payload data from advertising packet. You should never need to call this from client code, platform
      /// libraries should handle it.
      /// </summary>
      public static IEnumerable<AdvertisingDataItem> ParseAdvertisingPayloadData( Byte[] advD )
      {
         var records = new List<AdvertisingDataItem>();
         var index = 0;
         while(index < advD?.Length)
         {
            var length = advD[index];
            index++;
            if(length > 0)
            {
               if(!(advD.Length > index + length))
               {
                  throw new InvalidDataException(
                     "Advertising data specifies length {0} but only has {1} bytes remaining"
                        .F( length, advD.Length ) );
               }
               var type = advD[index];
               var data = advD.Slice( index + 1, index + length );
               index += length;
               records.Add( new AdvertisingDataItem( (AdvertisingDataType)type, data ) );
            }
         }
         return records;
      }
   }
}