// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.core;
using nexus.core.logging;

namespace nexus.protocols.ble.adopted
{
   /// <summary>
   /// Stores known information about services, characteristics, and descriptors. Useful for lookup to get a name when you
   /// just have a GUID
   /// </summary>
   public class KnownAttributes
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
               "Error adding BLE attribute. type={3} att-1=\"{0}\" att-2=\"{1}\" error=\"{2}\"",
               item.Description,
               m_items.Get( item.Id )?.Description,
               ex.Message,
               item.Type );
            item = null;
         }
         return item;
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddCharacteristic( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Characteristic, reservedKey, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddCharacteristic( Guid id, String name )
      {
         return Add( GattAttributeType.Characteristic, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddCharacteristic( String id, String name )
      {
         return Add( GattAttributeType.Characteristic, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddDescriptor( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Descriptor, reservedKey, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddDescriptor( Guid id, String name )
      {
         return Add( GattAttributeType.Descriptor, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddDescriptor( String id, String name )
      {
         return Add( GattAttributeType.Descriptor, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddService( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Service, reservedKey, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public IGattAttribute AddService( Guid id, String name )
      {
         return Add( GattAttributeType.Service, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
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

      private IGattAttribute Add( GattAttributeType type, Guid id, String name )
      {
         var item = new KnownAttribute( id, name, type );
         Add( item );
         return item;
      }

      private IGattAttribute Add( GattAttributeType type, String id, String name )
      {
         return Add( type, Guid.Parse( id ), name );
      }

      private IGattAttribute Add( GattAttributeType type, UInt16 reservedKey, String name )
      {
         return Add( type, reservedKey.CreateGuidFromAdoptedKey(), name );
      }
   }
}