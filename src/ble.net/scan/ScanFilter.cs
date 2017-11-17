// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// Create a new <see cref="IScanFilter" /> using an object initializer or a fluent builder interface.
   /// </summary>
   /// <inheritdoc cref="IScanFilter" />
   public sealed class ScanFilter : IScanFilter
   {
      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      /// <remarks>Syntax sugar for <c>new ScanFilter {IgnoreRepeatBroadcasts = true}</c></remarks>
      public static readonly IScanFilter UniqueBroadcastsOnly = new ScanFilter {IgnoreRepeatBroadcasts = true};

      private ISet<Guid> m_advertisedServiceIsInList;

      /// <inheritdoc />
      public ScanFilter()
      {
         m_advertisedServiceIsInList = new HashSet<Guid>();
      }

      /// <inheritdoc />
      /// <summary>
      /// Construct a new <see cref="ScanFilter" /> from the provided <paramref name="source" />
      /// </summary>
      public ScanFilter( IScanFilter source )
      {
         m_advertisedServiceIsInList = source.AdvertisedServiceIsInList == null
            ? null
            : new HashSet<Guid>( source.AdvertisedServiceIsInList );
         AdvertisedDeviceName = source.AdvertisedDeviceName;
         IgnoreRepeatBroadcasts = source.IgnoreRepeatBroadcasts;
         AdvertisedManufacturerCompanyId = source.AdvertisedManufacturerCompanyId;
      }

      /// <inheritdoc />
      public String AdvertisedDeviceName { get; set; }

      /// <inheritdoc />
      public UInt16? AdvertisedManufacturerCompanyId { get; set; }

      /// <inheritdoc />
      public IList<Guid> AdvertisedServiceIsInList
      {
         get { return m_advertisedServiceIsInList.ToList(); }
         set { m_advertisedServiceIsInList = value == null ? new HashSet<Guid>() : new HashSet<Guid>( value ); }
      }

      /// <inheritdoc />
      public Boolean IgnoreRepeatBroadcasts { get; set; }

      /// <summary>
      /// Add an attribute GIUD. This will require BLE broadcasts to advertise at least one of the guids provided
      /// </summary>
      public ScanFilter AddAdvertisedService( Guid guid )
      {
         m_advertisedServiceIsInList.Add( guid );
         return this;
      }

      /// <summary>
      /// Add an attribute GIUD. This will require BLE broadcasts to advertise at least one of the guids provided
      /// </summary>
      /// <exception cref="ArgumentNullException">If <paramref name="guid" /> is null</exception>
      /// <exception cref="FormatException">If <paramref name="guid" /> is not properly formatted as a GUID</exception>
      public ScanFilter AddAdvertisedService( String guid )
      {
         m_advertisedServiceIsInList.Add( Guid.Parse( guid ) );
         return this;
      }

      /// <summary>
      /// Add a reserved attribute GIUD. This will require BLE broadcasts to advertise at least one of the guids provided
      /// </summary>
      public ScanFilter AddAdvertisedService( UInt16 reserved )
      {
         m_advertisedServiceIsInList.Add( reserved.CreateGuidFromAdoptedKey() );
         return this;
      }

      /// <summary>
      /// The broadcast advertisement is displaying this device name
      /// </summary>
      public ScanFilter SetAdvertisedDeviceName( String value )
      {
         AdvertisedDeviceName = value;
         return this;
      }

      /// <summary>
      /// The broadcast advertisement has manufacturer data matching this company id
      /// <see href="https://www.bluetooth.com/specifications/assigned-numbers/company-identifiers" />
      /// </summary>
      public ScanFilter SetAdvertisedManufacturerCompanyId( UInt16? value )
      {
         AdvertisedManufacturerCompanyId = value;
         return this;
      }

      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      public ScanFilter SetIgnoreRepeatBroadcasts( Boolean value )
      {
         IgnoreRepeatBroadcasts = value;
         return this;
      }
   }
}