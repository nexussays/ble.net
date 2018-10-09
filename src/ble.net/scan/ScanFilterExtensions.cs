// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Linq;
using nexus.protocols.ble.scan.advertisement;

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// Extension methods for <see cref="IScanFilter" /> and <see cref="ScanFilter" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class ScanFilterExtensions
   {
      /// <summary>
      /// Add an attribute GIUD. This will require BLE broadcasts to advertise at least one of the guids provided
      /// </summary>
      /// <exception cref="ArgumentNullException">If <paramref name="guid" /> is null</exception>
      /// <exception cref="FormatException">If <paramref name="guid" /> is not properly formatted as a GUID</exception>
      public static ScanFilter AddAdvertisedService( this ScanFilter filter, String guid )
      {
         return filter.AddAdvertisedService( Guid.Parse( guid ) );
      }

      /// <summary>
      /// Add a reserved attribute GIUD. This will require BLE broadcasts to advertise at least one of the guids provided
      /// </summary>
      public static ScanFilter AddAdvertisedService( this ScanFilter filter, UInt16 reserved )
      {
         return filter.AddAdvertisedService( reserved.CreateGuidFromAdoptedKey() );
      }

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
