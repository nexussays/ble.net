// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.gatt
{
   /// <summary>
   /// The GATT type a given ATT attribute represents.
   /// </summary>
   public enum GattAttributeType
   {
      /// <summary>
      /// Represents a GATT service
      /// </summary>
      Service = 0,
      /// <summary>
      /// Represents a GATT characteristic
      /// </summary>
      Characteristic,
      /// <summary>
      /// Represents a GATT descriptor
      /// </summary>
      Descriptor
   }
}