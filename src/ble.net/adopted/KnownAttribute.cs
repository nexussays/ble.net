// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.adopted
{
   /// <summary>
   /// Represents an ATT attribute (service, characteristic, or descriptor) that is of a known type and usage and has an
   /// associated name
   /// </summary>
   public class KnownAttribute : IGattAttribute
   {
      /// <summary>
      /// </summary>
      public KnownAttribute( Guid id, String description, GattAttributeType type )
      {
         Id = id;
         Description = description;
         Type = type;
      }

      /// <summary>
      /// A human-friendly description for this service
      /// </summary>
      public String Description { get; }

      /// <inheritdoc />
      public Guid Id { get; }

      /// <summary>
      /// The GATT type of this ATT attribute
      /// </summary>
      public GattAttributeType Type { get; }

      /// <inheritdoc />
      public override Boolean Equals( Object obj )
      {
         return Equals( obj as IGattAttribute );
      }

      /// <inheritdoc />
      public Boolean Equals( IGattAttribute other )
      {
         return !ReferenceEquals( null, other ) && other.Id.Equals( Id );
      }

      /// <inheritdoc />
      public override Int32 GetHashCode()
      {
         return Id.GetHashCode();
      }
   }
}