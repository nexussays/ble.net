// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;
using nexus.core;
using nexus.core.logging;

namespace nexus.protocols.ble.gatt.adopted
{
   /// <summary>
   /// Stores known information about services, characteristics, and descriptors. Useful for lookup to get a name when you
   /// just have a GUID (see <see cref="Get" />). Typically you will create a singleton instance in your application.
   /// <remarks>
   /// You can add your own GUIDs and also add GUIDs that have been adopted by the Bluetooth SIG by utilizing the extension
   /// methods <see cref="AdoptedServices.AddAdoptedServices" />,
   /// <see cref="AdoptedCharacteristics.AddAdoptedCharacteristics" />, and
   /// <see cref="AdoptedDescriptors.AddAdoptedDescriptors" />
   /// </remarks>
   /// </summary>
   public class KnownAttributes : IEnumerable<KeyValuePair<Guid, IGattAttribute>>
   {
      /// <summary>
      /// The dictionary of registered attributes
      /// </summary>
      protected readonly IDictionary<Guid, IGattAttribute> m_items;

      /// <summary>
      /// </summary>
      public KnownAttributes()
      {
         m_items = new Dictionary<Guid, IGattAttribute>();
      }

      /// <summary>
      /// Add a <see cref="IGattAttribute" />
      /// </summary>
      public IGattAttribute Add( IGattAttribute item )
      {
         if(item == null)
         {
            return null;
         }

         try
         {
            m_items.Add( item.Id, item );
         }
         catch(Exception ex)
         {
            Log.Warn(
               "Unable to add BLE attribute to {4}. type={3} att-1=\"{0}\" att-2=\"{1}\" error=\"{2}\"",
               item.Description,
               m_items.Get( item.Id )?.Description,
               ex.Message,
               item.Type,
               GetType().Name );
            item = null;
         }
         return item;
      }

      /// <summary>
      /// Add information for a known GATT attribute so it can be looked up later
      /// </summary>
      public IGattAttribute Add( GattAttributeType type, Guid id, String name )
      {
         return Add( new GattAttribute( type, id, name ) );
      }

      /// <summary>
      /// Add information for a known GATT attribute so it can be looked up later
      /// </summary>
      public IGattAttribute Add( GattAttributeType type, String id, String name )
      {
         return Add( type, Guid.Parse( id ), name );
      }

      /// <summary>
      /// Add information for a known GATT attribute so it can be looked up later
      /// </summary>
      public IGattAttribute Add( GattAttributeType type, UInt16 reservedKey, String name )
      {
         return Add( type, reservedKey.CreateGuidFromAdoptedKey(), name );
      }

      /// <summary>
      /// Add information for a known GATT characteristic so it can be looked up later
      /// </summary>
      public IGattAttribute AddCharacteristic( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Characteristic, reservedKey, name );
      }

      /// <summary>
      /// Add information for a known GATT characteristic so it can be looked up later
      /// </summary>
      public IGattAttribute AddCharacteristic( Guid id, String name )
      {
         return Add( GattAttributeType.Characteristic, id, name );
      }

      /// <summary>
      /// Add information for a known GATT characteristic so it can be looked up later
      /// </summary>
      public IGattAttribute AddCharacteristic( String id, String name )
      {
         return Add( GattAttributeType.Characteristic, id, name );
      }

      /// <summary>
      /// Add information for a known GATT descriptor so it can be looked up later
      /// </summary>
      public IGattAttribute AddDescriptor( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Descriptor, reservedKey, name );
      }

      /// <summary>
      /// Add information for a known GATT descriptor so it can be looked up later
      /// </summary>
      public IGattAttribute AddDescriptor( Guid id, String name )
      {
         return Add( GattAttributeType.Descriptor, id, name );
      }

      /// <summary>
      /// Add information for a known GATT descriptor so it can be looked up later
      /// </summary>
      public IGattAttribute AddDescriptor( String id, String name )
      {
         return Add( GattAttributeType.Descriptor, id, name );
      }

      /// <summary>
      /// Add information for a known GATT service so it can be looked up later
      /// </summary>
      public IGattAttribute AddService( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Service, reservedKey, name );
      }

      /// <summary>
      /// Add information for a known GATT service so it can be looked up later
      /// </summary>
      public IGattAttribute AddService( Guid id, String name )
      {
         return Add( GattAttributeType.Service, id, name );
      }

      /// <summary>
      /// Add information for a known GATT service so it can be looked up later
      /// </summary>
      public IGattAttribute AddService( String id, String name )
      {
         return Add( GattAttributeType.Service, id, name );
      }

      /// <summary>
      /// Retrieve any known information about the attribute with the given GUID
      /// </summary>
      public IGattAttribute Get( Guid id )
      {
         return m_items.Get( id );
      }

      /// <inheritdoc />
      public IEnumerator<KeyValuePair<Guid, IGattAttribute>> GetEnumerator()
      {
         return m_items.GetEnumerator();
      }

      /// <inheritdoc />
      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      /// <summary>
      /// Syntax sugar to create a new <see cref="KnownAttributes" /> with adopted attributes added by calling
      /// <see cref="AdoptedServices.AddAdoptedServices" />, <see cref="AdoptedCharacteristics.AddAdoptedCharacteristics" />, and
      /// <see cref="AdoptedDescriptors.AddAdoptedDescriptors" />
      /// </summary>
      public static KnownAttributes CreateWithAdoptedAttributes()
      {
         var result = new KnownAttributes();
         result.AddAdoptedServices();
         result.AddAdoptedCharacteristics();
         result.AddAdoptedDescriptors();
         return result;
      }
   }

   /// <summary>
   /// Extension methods for <see cref="KnownAttributes" />
   /// </summary>
   public static class KnownAttributesExtensions
   {
      /// <summary>
      /// Returns the description of the known GUID value if it is known, else the GUID formatted according to
      /// <paramref name="guidFormatString" />
      /// </summary>
      public static String GetDescriptionOrGuid( this KnownAttributes known, Guid id, String guidFormatString = null )
      {
         return known.Get( id )?.Description ?? id.ToString( guidFormatString );
      }
   }
}