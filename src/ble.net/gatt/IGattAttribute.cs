// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.gatt
{
   /// <summary>
   /// A GUID and type that represents a GATT attribute
   /// </summary>
   public interface IGattAttribute : IEquatable<IGattAttribute>
   {
      /// <summary>
      /// A human-friendly description for this service, if available
      /// </summary>
      String Description { get; }

      /// <summary>
      /// The unique UUID for this attribute
      /// </summary>
      Guid Id { get; }

      /// <summary>
      /// The GATT type of this ATT attribute: service, characteristic, or descriptor
      /// </summary>
      GattAttributeType Type { get; }
   }

   /// <summary>
   /// Extension methods for <see cref="IGattAttribute" />
   /// </summary>
   public static class GattAttributeExtensions
   {
      /// <summary>
      /// Convert to tuple
      /// </summary>
      public static void Deconstruct( IGattAttribute att, out Guid id, out GattAttributeType type,
                                      out String description )
      {
         description = att.Description;
         id = att.Id;
         type = att.Type;
      }
   }

   /// <summary>
   /// Represents an ATT attribute (service, characteristic, or descriptor)
   /// </summary>
   /// <remarks>Be sure to never reference this type and use <see cref="IGattAttribute"/> instead</remarks>
   public sealed class GattAttribute : IGattAttribute
   {
      /// <summary>
      /// </summary>
      public GattAttribute( GattAttributeType type, Guid id, String description )
      {
         Id = id;
         Description = description;
         Type = type;
      }

      /// <inheritdoc />
      public String Description { get; }

      /// <inheritdoc />
      public Guid Id { get; }

      /// <inheritdoc />
      public GattAttributeType Type { get; }

      /// <inheritdoc />
      public override Boolean Equals( Object obj )
      {
         return Equals(obj as IGattAttribute);
      }

      /// <inheritdoc />
      public Boolean Equals( IGattAttribute other )
      {
         return !ReferenceEquals(null, other) && other.Id.Equals(Id);
      }

      /// <inheritdoc />
      public override Int32 GetHashCode()
      {
         return Id.GetHashCode();
      }
   }
}