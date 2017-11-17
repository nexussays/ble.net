// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.protocols.ble.scan.advertisement;

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// A discovered BLE peripheral device and the advertising data it is broadcasting
   /// </summary>
   public interface IBlePeripheral : IEquatable<IBlePeripheral>
   {
      /// <summary>
      /// The <see cref="IBlePeripheral" />'s 6-byte MAC address, or an empty byte array if the address is unavailable or not
      /// provided on the platform (e.g., iOS). If you want to re-discover this <see cref="IBlePeripheral" />, see
      /// <see cref="DeviceId" />.
      /// </summary>
      Byte[ /*6*/] Address { get; }

      /// <summary>
      /// <c>true</c> if the advertised <see cref="Address" /> is a randomly generated value, <c>false</c> if
      /// <see cref="Address" /> is a
      /// fixed MAC address, <c>null</c> if the underlying platform does not provide this information (i.e., it could be either
      /// random or a MAC address)
      /// </summary>
      Boolean? AddressIsRandom { get; }

      /// <summary>
      /// The advertising data in the peripheral's advertising broadcast.
      /// </summary>
      IBleAdvertisement Advertisement { get; }

      /// <summary>
      /// A unique identifier for this <see cref="IBlePeripheral" /> that can be used to discover it again.
      /// </summary>
      /// <remarks>
      /// Note that this value can be inconsistent between platforms (iOS, for example provides random IDs that are unique to the
      /// given iPhone/iPad/etc), so you cannot share <see cref="DeviceId" /> between iOS and Android/UWP
      /// app to discover the same <see cref="IBlePeripheral" />.
      /// </remarks>
      Guid DeviceId { get; }

      /// <summary>
      /// Received signal strenth indicator, in decibels
      /// </summary>
      Int32 Rssi { get; }
   }
}