// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using nexus.protocols.ble.scan.advertisement;

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// Filter to provide to
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ScanForBroadcasts(nexus.protocols.ble.scan.IScanFilter,System.IObserver{nexus.protocols.ble.scan.IBlePeripheral},System.Threading.CancellationToken)" />
   /// which will only report found devices that match the filter. See <see cref="ScanFilter" />
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
      /// The broadcast advertisement lists a service that is contained in this list.
      /// </summary>
      IList<Guid> AdvertisedServiceIsInList { get; }

      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      Boolean IgnoreRepeatBroadcasts { get; }
   }

   /// <summary>
   /// Extension methods for <see cref="IScanFilter" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class ScanFilterExtensions
   {
      /// <summary>
      /// Returns true if the provided advertisement passes the scan filter
      /// </summary>
      public static Boolean Passes( this IScanFilter filter, IBleAdvertisement advertisement )
      {
         var services = filter.AdvertisedServiceIsInList;
         if(services != null && services.Count > 0 && !services.Any( guid => advertisement.Services.Contains( guid ) ))
         {
            return false;
         }

         if(filter.AdvertisedDeviceName != null && advertisement.DeviceName != filter.AdvertisedDeviceName)
         {
            return false;
         }

         var companyId = filter.AdvertisedManufacturerCompanyId;
         // ReSharper disable once ConvertIfStatementToReturnStatement
         if(companyId != null && advertisement.ManufacturerSpecificData.All( x => x.CompanyId != companyId.Value ))
         {
            return false;
         }

         return true;
      }
   }
}