// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
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
}