// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.protocols.ble
{
   /// <summary>
   /// Filter to provide to
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ScanForBroadcasts(ScanFilter,System.IObserver{nexus.protocols.ble.IBlePeripheral},System.Threading.CancellationToken)" />
   /// which will only report found devices that match the filter. See: See <see cref="ScanFilter.Factory" />
   /// </summary>
   public interface IScanFilter
   {
      /// <summary>
      /// The broadcast advertisement is displaying this device name
      /// </summary>
      String AdvertisedDeviceName { get; }

      /// <summary>
      /// The broadcast advertisement has manufacturer data matching this company id
      /// <see href="https://www.bluetooth.com/specifications/assigned-numbers/company-identifiers" />
      /// </summary>
      UInt16? AdvertisedManufacturerCompanyId { get; }

      /// <summary>
      /// The broadcast advertisement lists a service that is contained in this list
      /// </summary>
      IEnumerable<Guid> AdvertisedServiceIsInList { get; }

      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      Boolean IgnoreRepeatBroadcasts { get; }
   }
}