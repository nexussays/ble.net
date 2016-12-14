// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using nexus.core;
using nexus.protocols.ble.advertisement;

namespace nexus.protocols.ble
{
   /// <summary>
   /// Utility methods for BLE
   /// </summary>
   public static class BluetoothLowEnergyUtils
   {
      public static readonly Guid CharacteristicExtendedPropertiesDescriptor = CreateGuidFromAdoptedKey( 0x2900 );
      public static readonly Guid CharacteristicUserDescriptionDescriptor = CreateGuidFromAdoptedKey( 0x2901 );
      /// <summary>
      /// Client characteristic configuration descriptor GUID
      /// </summary>
      /// <see
      ///    href="https://www.bluetooth.com/specifications/gatt/viewer?attributeXmlFile=org.bluetooth.descriptor.gatt.client_characteristic_configuration.xml" />
      public static readonly Guid ClientCharacteristicConfigurationDescriptor = CreateGuidFromAdoptedKey( 0x2902 );
      public static readonly Guid ServerCharacteristicConfigurationDescriptor = CreateGuidFromAdoptedKey( 0x2903 );
      public static readonly Guid CharacteristicFormatDescriptor = CreateGuidFromAdoptedKey( 0x2904 );
      public static readonly Guid CharacteristicAggregateFormatDescriptor = CreateGuidFromAdoptedKey( 0x2905 );

      /// <summary>
      /// The default connection timeout used if no timeout is provided
      /// </summary>
      public static readonly TimeSpan DefaultConnectionTimeout = TimeSpan.FromSeconds( 5 );

      /// <summary>
      /// The default time to scan if no timeout is provided
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
         if(address.IsNullOrEmptyOrNullByte() || address.Length != 6)
         {
            throw new ArgumentException( "Address must be an array of 6 bytes." );
         }
         return
            new Guid(
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
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout after the default time;
      /// <see cref="DefaultConnectionTimeout" />
      /// </summary>
      public static Task<IBleGattServer> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, Guid id )
      {
         return ConnectToDevice( adapter, id, DefaultConnectionTimeout );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<IBleGattServer> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, Guid id,
                                                          TimeSpan timeout )
      {
         return adapter.ConnectToDevice( id, new CancellationTokenSource( timeout ).Token );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout after the default time;
      /// <see cref="DefaultConnectionTimeout" />
      /// </summary>
      public static Task<IBleGattServer> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, IBlePeripheral device )
      {
         return ConnectToDevice( adapter, device, DefaultConnectionTimeout );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<IBleGattServer> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, IBlePeripheral device,
                                                          TimeSpan timeout )
      {
         return adapter.ConnectToDevice( device, new CancellationTokenSource( timeout ).Token );
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
         if(adoptedKey == null)
         {
            throw new ArgumentNullException( nameof( adoptedKey ) );
         }
         if(adoptedKey.Length != 4)
         {
            throw new ArgumentException(
               "A Bluetooth adopted key must be a 4-character base16-encoded string",
               nameof( adoptedKey ) );
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
               nameof( adoptedKey ),
               ex );
         }
      }

      public static Boolean IsReservedKey( this Guid id )
      {
         var bytes = id.ToByteArray();
         bytes[2] = 0;
         bytes[3] = 0;
         return s_adoptedKeyBase.Equals( id );
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
               var type = advD[index];
               var data = advD.Slice( index + 1, index + length );
               index += length;
               records.Add( new AdvertisingDataItem( (AdvertisingDataType)type, data ) );
            }
         }
         return records;
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">cancel scan after this length of time</param>
      public static Task ScanForDevices( this IBluetoothLowEnergyAdapter adapter,
                                         IObserver<IBlePeripheral> advertisementDiscovered, TimeSpan timeout )
      {
         return adapter.ScanForDevices( advertisementDiscovered, new CancellationTokenSource( timeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after <see cref="DefaultScanTimeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      public static Task ScanForDevices( this IBluetoothLowEnergyAdapter adapter,
                                         IObserver<IBlePeripheral> advertisementDiscovered )
      {
         return ScanForDevices( adapter, advertisementDiscovered, DefaultScanTimeout );
      }
   }
}