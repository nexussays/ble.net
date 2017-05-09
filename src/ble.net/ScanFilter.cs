// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using nexus.protocols.ble.advertisement;

namespace nexus.protocols.ble
{
   /// <summary>
   /// Filter used when scanning for broadcasts. If broadcast advertisements do not match the scan filter, they will not be
   /// reported to your observer. See <see cref="Factory" />.
   /// </summary>
   public sealed class ScanFilter : IScanFilter
   {
      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      /// <remarks>Syntax sugar for <c>new ScanFilter.Factory {IgnoreRepeatBroadcasts = true}.CreateFilter()</c></remarks>
      public static readonly ScanFilter UniqueBroadcastsOnly =
         new Factory {IgnoreRepeatBroadcasts = true}.CreateFilter();

      private readonly List<Guid> m_advertisedServiceIsInList;

      private ScanFilter( IScanFilter factory )
      {
         m_advertisedServiceIsInList = factory.AdvertisedServiceIsInList == null
            ? null
            : new List<Guid>( factory.AdvertisedServiceIsInList );
         AdvertisedDeviceName = factory.AdvertisedDeviceName;
         IgnoreRepeatBroadcasts = factory.IgnoreRepeatBroadcasts;
         AdvertisedManufacturerCompanyId = factory.AdvertisedManufacturerCompanyId;
      }

      /// <inheritdoc />
      public String AdvertisedDeviceName { get; }

      /// <inheritdoc />
      public UInt16? AdvertisedManufacturerCompanyId { get; }

      /// <inheritdoc />
      public IEnumerable<Guid> AdvertisedServiceIsInList => m_advertisedServiceIsInList;

      /// <inheritdoc />
      public Boolean IgnoreRepeatBroadcasts { get; }

      /// <summary>
      /// Returns true if the provided advertisement passes the scan filter
      /// </summary>
      /// <param name="advertisement"></param>
      /// <returns></returns>
      public Boolean Passes( IBleAdvertisement advertisement )
      {
         if(m_advertisedServiceIsInList != null && m_advertisedServiceIsInList.Count > 0)
         {
            var passes = false;
            foreach(var guid in m_advertisedServiceIsInList)
            {
               if(advertisement.Services.Contains( guid ))
               {
                  passes = true;
                  break;
               }
            }
            if(!passes)
            {
               return false;
            }
         }

         if(AdvertisedDeviceName != null && advertisement.DeviceName != AdvertisedDeviceName)
         {
            return false;
         }

         if(AdvertisedManufacturerCompanyId != null &&
            advertisement.ManufacturerSpecificData.All( x => x.CompanyId != AdvertisedManufacturerCompanyId.Value ))
         {
            return false;
         }

         return true;
      }

      /// <summary>
      /// Factory to create a new <see cref="ScanFilter" />
      /// </summary>
      public class Factory : IScanFilter
      {
         /// <inheritdoc />
         public String AdvertisedDeviceName { get; set; }

         /// <inheritdoc />
         public UInt16? AdvertisedManufacturerCompanyId { get; set; }

         /// <inheritdoc />
         public IEnumerable<Guid> AdvertisedServiceIsInList { get; set; }

         /// <inheritdoc />
         public Boolean IgnoreRepeatBroadcasts { get; set; }

         /// <summary>
         /// Create a new <see cref="ScanFilter" /> with the provided <see cref="Factory" />
         /// </summary>
         public ScanFilter CreateFilter()
         {
            return new ScanFilter( this );
         }

         /// <inheritdoc cref="AdvertisedDeviceName" />
         public Factory SetAdvertisedDeviceName( String value )
         {
            AdvertisedDeviceName = value;
            return this;
         }

         /// <inheritdoc cref="AdvertisedManufacturerCompanyId" />
         public Factory SetAdvertisedManufacturerCompanyId( UInt16? value )
         {
            AdvertisedManufacturerCompanyId = value;
            return this;
         }

         /// <inheritdoc cref="IgnoreRepeatBroadcasts" />
         public Factory SetIgnoreRepeatBroadcasts( Boolean value )
         {
            IgnoreRepeatBroadcasts = value;
            return this;
         }
      }
   }
}