// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
{
   /// <summary>
   /// A discovered BLE peripheral and its advertising data
   /// </summary>
   public interface IBlePeripheral : IEquatable<IBlePeripheral>
   {
      /// <summary>
      /// The advertising peripheral's 6-byte MAC address, or an empty byte array if the address is unavailable
      /// </summary>
      Byte[ /*6*/] Address { get; }

      /// <summary>
      /// True if the advertised address <see cref="Address" /> is a randomly generated value, false if it is a fixed MAC
      /// address. If null, then the underlying platform does not provide this information
      /// </summary>
      Boolean? AddressIsRandom { get; }

      /// <summary>
      /// A list of all the advertising data structures parsed from the peripheral's advertising broadcast.
      /// </summary>
      IBlePeripheralAdvertisement Advertisement { get; }

      /// <summary>
      /// Unique identifier for the device in Guid form. Typically the device's MAC address (<see cref="Address" />)  but some
      /// platforms (eg, iOS) do not provide the address so a system-provided ID is used instead.
      /// </summary>
      Guid DeviceId { get; }

      /// <summary>
      /// Received signal strenth indicator, in decibels
      /// </summary>
      Int32 Rssi { get; }
   }
}