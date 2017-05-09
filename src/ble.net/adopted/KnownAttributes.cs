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
   /// Stores known information about services, characteristics, and descriptors. Useful for lookup to get a name when you just have a GUID
   /// </summary>
   public class KnownAttributes
   {
      protected readonly Dictionary<Guid, KnownAttribute> m_items;

      /// <summary>
      /// </summary>
      public KnownAttributes()
      {
         m_items = new Dictionary<Guid, KnownAttribute>();
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddCharacteristic( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Characteristic, reservedKey, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddCharacteristic( Guid id, String name )
      {
         return Add( GattAttributeType.Characteristic, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddCharacteristic( String id, String name )
      {
         return Add( GattAttributeType.Characteristic, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddDescriptor( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Descriptor, reservedKey, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddDescriptor( Guid id, String name )
      {
         return Add( GattAttributeType.Descriptor, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddDescriptor( String id, String name )
      {
         return Add( GattAttributeType.Descriptor, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddService( UInt16 reservedKey, String name )
      {
         return Add( GattAttributeType.Service, reservedKey, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddService( Guid id, String name )
      {
         return Add( GattAttributeType.Service, id, name );
      }

      /// <summary>
      /// Add the provided information to the known attributes so it can easily be looked up later. Returns an object
      /// representing the known information about this attribute or null of the information already exists.
      /// </summary>
      public KnownAttribute AddService( String id, String name )
      {
         return Add( GattAttributeType.Service, id, name );
      }

      /// <summary>
      /// Retrieve any known information about the attribute with the given GUID
      /// </summary>
      public KnownAttribute Get( Guid id )
      {
         return m_items.Get( id );
      }

      private KnownAttribute Add( GattAttributeType type, Guid id, String name )
      {
         KnownAttribute item;
         try
         {
            item = new KnownAttribute( id, name, type );
            m_items.Add( id, item );
         }
         catch(Exception ex)
         {
            Log.Warn(
               "Error adding BLE attribute. type={3} att-1=\"{0}\" att-2=\"{1}\" error=\"{2}\"",
               name,
               m_items.Get( id )?.Description,
               ex.Message,
               type );
            item = null;
         }
         return item;
      }

      private KnownAttribute Add( GattAttributeType type, String id, String name )
      {
         return Add( type, Guid.Parse( id ), name );
      }

      private KnownAttribute Add( GattAttributeType type, UInt16 reservedKey, String name )
      {
         return Add( type, reservedKey.CreateGuidFromAdoptedKey(), name );
      }
   }
}