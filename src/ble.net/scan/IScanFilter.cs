// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// Filter to provide to
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ScanForBroadcasts(System.IObserver{nexus.protocols.ble.scan.IBlePeripheral},System.Threading.CancellationToken)" />
   /// which will only report found devices that match the filter. See <see cref="ScanFilter" /> for the stnadard
   /// implementation.
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
      /// The broadcast advertisement must list a service that is contained in this list in order to pass the filter.
      /// </summary>
      IList<Guid> AdvertisedServiceIsInList { get; }

      [ObsoleteEx(
         TreatAsErrorFromVersion = "2.0.0",
         RemoveInVersion = "2.1.0",
         ReplacementTypeOrMember = nameof(ScanSettings.IgnoreRepeatBroadcasts) )]
      Boolean IgnoreRepeatBroadcasts { get; }
   }
}
