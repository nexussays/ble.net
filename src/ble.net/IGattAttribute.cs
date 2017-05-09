// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
{
   /// <summary>
   /// A GUID and type that tepresents a GATT attribute
   /// </summary>
   public interface IGattAttribute : IEquatable<IGattAttribute>
   {
      /// <summary>
      /// The unique UUID for this attribute
      /// </summary>
      Guid Id { get; }

      /// <summary>
      /// The type of attribute, service, characteristic, or descriptor
      /// </summary>
      GattAttributeType Type { get; }
   }
}