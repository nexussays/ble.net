// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.scan.advertisement
{
   /// <summary>
   /// The type of an entry in the 31-byte advertising PDU payload.
   /// Assigned numbers are used in GAP for inquiry response, EIR data type values, manufacturer-specific data, advertising
   /// data, low energy UUIDs and appearance characteristics, and class of device.
   /// </summary>
   /// <see href="https://www.bluetooth.com/specifications/assigned-numbers/generic-access-profile" />
   public enum AdvertisingDataType
   {
      /// <summary>
      /// Flags for discoverability. <see cref="AdvertisingDataFlags" />
      /// </summary>
      Flags = 0x01,
      /// <summary>
      /// Incomplete List of 16-bit Service Class UUIDs
      /// </summary>
      IncompleteServices16 = 0x02,
      /// <summary>
      /// Complete List of 16-bit Service Class UUIDs
      /// </summary>
      CompleteServices16 = 0x03,
      /// <summary>
      /// Incomplete List of 32-bit Service Class UUIDs
      /// </summary>
      IncompleteServices32 = 0x04,
      /// <summary>
      /// Complete List of 32-bit Service Class UUIDs
      /// </summary>
      CompleteServices32 = 0x05,
      /// <summary>
      /// Incomplete List of 128-bit Service Class UUIDs
      /// </summary>
      IncompleteServices128 = 0x06,
      /// <summary>
      /// Complete List of 128-bit Service Class UUIDs
      /// </summary>
      CompleteServices128 = 0x07,
      /// <summary>
      /// Shortened Local Name
      /// </summary>
      ShortenedLocalName = 0x08,
      /// <summary>
      /// Complete Local Name
      /// </summary>
      CompleteLocalName = 0x09,
      /// <summary>
      /// Tx Power Level
      /// </summary>
      TxPowerLevel = 0x0A,
      /// <summary>
      /// Class of Device
      /// </summary>
      ClassOfDevice = 0x0D,
      /// <summary>
      /// Simple Pairing Hash C-192
      /// </summary>
      SimplePairingHashC192 = 0x0E,
      /// <summary>
      /// Simple Pairing Randomizer R-192
      /// </summary>
      SimplePairingRandomizerR192 = 0x0F,
      /// <summary>
      /// Device ID
      /// </summary>
      DeviceId = 0x10,
      /// <summary>
      /// Security Manager TK Value
      /// </summary>
      SecurityManagerTkValue = 0x10,
      /// <summary>
      /// /Security Manager Out of Band Flags
      /// </summary>
      SecurityManagerOutOfBandFlags = 0x11,
      /// <summary>
      /// Slave Connection Interval Range
      /// </summary>
      SlaveConnectionIntervalRange = 0x12,
      /// <summary>
      /// List of 16-bit Service Solicitation UUIDs
      /// </summary>
      ServiceSolicitations16 = 0x14,
      /// <summary>
      /// List of 32-bit Service Solicitation UUIDs
      /// </summary>
      ServiceSolicitations32 = 0x1F,
      /// <summary>
      /// List of 128-bit Service Solicitation UUIDs
      /// </summary>
      ServiceSolicitations128 = 0x15,
      /// <summary>
      /// Service Data - 16-bit UUID
      /// </summary>
      ServiceData16 = 0x16,
      /// <summary>
      /// Service Data - 32-bit UUID
      /// </summary>
      ServiceData32 = 0x20,
      /// <summary>
      /// Service Data - 128-bit UUID
      /// </summary>
      ServiceData128 = 0x21,
      /// <summary>
      /// LE Secure Connections Confirmation Value
      /// </summary>
      LeSecureConnectionsConfirmationValue = 0x22,
      /// <summary>
      /// LE Secure Connections Random Value
      /// </summary>
      LeSecureConnectionsRandomValue = 0x23,
      /// <summary>
      /// URI
      /// </summary>
      Uri = 0x24,
      /// <summary>
      /// Indoor Positioning
      /// </summary>
      IndoorPositioning = 0x25,
      /// <summary>
      /// Transport Discovery Data
      /// </summary>
      TransportDiscoveryData = 0x26,
      /// <summary>
      /// Public Target Address
      /// </summary>
      PublicTargetAddress = 0x17,
      /// <summary>
      /// Random Target Address
      /// </summary>
      RandomTargetAddress = 0x18,
      /// <summary>
      /// Appearance
      /// </summary>
      Appearance = 0x19,
      /// <summary>
      /// Advertising Interval
      /// </summary>
      AdvertisingInterval = 0x1A,
      /// <summary>
      /// ​LE Bluetooth Device Address
      /// </summary>
      LeBluetoothDeviceAddress = 0x1B,
      /// <summary>
      /// LE Role
      /// </summary>
      LeRole = 0x1C,
      /// <summary>
      /// Simple Pairing Hash C-256
      /// </summary>
      SimplePairingHash = 0x1D,
      /// <summary>
      /// ​Simple Pairing Randomizer R-256
      /// </summary>
      SimplePairingRandomizer = 0x1E,
      /// <summary>
      /// 3D Information Data
      /// </summary>
      InformationData3d = 0x3D,
      /// <summary>
      /// Manufacturer Specific Data
      /// </summary>
      ManufacturerSpecificData = 0xFF
   }
}