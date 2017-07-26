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

      private readonly HashSet<Guid> m_advertisedServiceIsInList;

      private ScanFilter( Factory factory )
      {
         m_advertisedServiceIsInList = factory.AdvertisedServiceIsInList == null
            ? null
            : new HashSet<Guid>( factory.AdvertisedServiceIsInList );
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
         if(m_advertisedServiceIsInList != null && m_advertisedServiceIsInList.Count > 0 &&
            !m_advertisedServiceIsInList.Any( guid => advertisement.Services.Contains( guid ) ))
         {
            return false;
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
      public class Factory
      {
         /// <inheritdoc cref="IScanFilter.AdvertisedDeviceName" />
         public String AdvertisedDeviceName { get; set; }

         /// <inheritdoc cref="IScanFilter.AdvertisedManufacturerCompanyId" />
         public UInt16? AdvertisedManufacturerCompanyId { get; set; }

         /// <inheritdoc cref="IScanFilter.AdvertisedServiceIsInList" />
         public IList<Guid> AdvertisedServiceIsInList { get; set; }

         /// <inheritdoc cref="IScanFilter.IgnoreRepeatBroadcasts" />
         public Boolean IgnoreRepeatBroadcasts { get; set; }

         /// <inheritdoc cref="IScanFilter.AdvertisedServiceIsInList" />
         public Factory AddAdvertisedService( Guid guid )
         {
            if(AdvertisedServiceIsInList == null)
            {
               AdvertisedServiceIsInList = new List<Guid>();
            }
            AdvertisedServiceIsInList.Add( guid );
            return this;
         }

         /// <summary>
         /// Create a new <see cref="ScanFilter" /> with the provided <see cref="Factory" />
         /// </summary>
         public ScanFilter CreateFilter()
         {
            return new ScanFilter( this );
         }

         /// <inheritdoc cref="IScanFilter.AdvertisedDeviceName" />
         public Factory SetAdvertisedDeviceName( String value )
         {
            AdvertisedDeviceName = value;
            return this;
         }

         /// <inheritdoc cref="IScanFilter.AdvertisedManufacturerCompanyId" />
         public Factory SetAdvertisedManufacturerCompanyId( UInt16? value )
         {
            AdvertisedManufacturerCompanyId = value;
            return this;
         }

         /// <inheritdoc cref="IScanFilter.IgnoreRepeatBroadcasts" />
         public Factory SetIgnoreRepeatBroadcasts( Boolean value )
         {
            IgnoreRepeatBroadcasts = value;
            return this;
         }

         /// <summary>
         /// Create scan filter from factory so we can pass it in without requiring consumers to call CreateFilter
         /// </summary>
         public static implicit operator ScanFilter( Factory factory )
         {
            return factory?.CreateFilter();
         }
      }
   }
}