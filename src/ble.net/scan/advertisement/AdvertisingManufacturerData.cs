// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;

namespace nexus.protocols.ble.scan.advertisement
{
   /// <summary>
   /// Manufacturer data in an advertising payload
   /// </summary>
   public struct AdvertisingManufacturerData
   {
      /// <summary>
      /// </summary>
      public AdvertisingManufacturerData( UInt16 company, Byte[] data )
      {
         CompanyId = company;
         Data = data;
      }

      /// <summary>
      /// Company identifiers are unique numbers assigned by the Bluetooth SIG to member companies requesting one.
      /// <see href="https://www.bluetooth.com/specifications/assigned-numbers/company-identifiers" />
      /// </summary>
      public UInt16? CompanyId { get; }

      /// <summary>
      /// The manufacturer data being advertised
      /// </summary>
      public Byte[] Data { get; }
   }

   /// <summary>
   /// Extension methods for <see cref="AdvertisingManufacturerData" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class AdvertisingManufacturerDataExtensions
   {
      /// <summary>
      /// Return the name of the company registered for <paramref name="adv.CompanyId" />
      /// <remarks>Syntax sugar for <c>RegisteredCompanyIds.GetCommonName( adv.CompanyId )</c></remarks>
      /// </summary>
      public static String CompanyName( this AdvertisingManufacturerData adv )
      {
         return RegisteredCompanyIds.GetCommonName( adv.CompanyId );
      }
   }
}