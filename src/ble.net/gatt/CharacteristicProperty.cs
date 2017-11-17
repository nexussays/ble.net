// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.gatt
{
   /// <summary>
   /// Property flags for GATT characteristics. See utility methods: <see cref="BleGattUtils.CanIndicate" />,
   /// <see cref="BleGattUtils.CanNotify" />, <see cref="BleGattUtils.CanRead" />,
   /// <see cref="BleGattUtils.CanWrite" />
   /// </summary>
   /// <remarks>
   /// Take note that these properties are for the characteristic in the GATT layer,
   /// these are not *permissions* which are in the ATT layer.
   /// </remarks>
   [Flags]
   public enum CharacteristicProperty
   {
      /// <summary>
      /// If set, permists broadcast of the characteristic value using Server Characteristic Confiuguration Descriptor
      /// </summary>
      Broadcast = 1,

      /// <summary>
      /// If set, permists read of the characteristic value
      /// </summary>
      Read = 2,

      /// <summary>
      /// If set, permists writes of the characteristic value without response
      /// </summary>
      WriteNoResponse = 4,

      /// <summary>
      /// If set, permists writes of the characteristic value with response
      /// </summary>
      Write = 8,

      /// <summary>
      /// If set, permists notifications of a characteristic value without acknowledgement
      /// </summary>
      Notify = 16,

      /// <summary>
      /// If set, permists indications of a characteristic value with acknowledgement
      /// </summary>
      Indicate = 32,

      /// <summary>
      /// If set, permists signed writes to the characteristic value
      /// </summary>
      SignedWrite = 64,

      /// <summary>
      /// If set, additional characteristic properties are defined
      /// </summary>
      /// <see cref="CharacteristicExtendedProperty" />
      ExtendedProperties = 128
   }

   /// <summary>
   /// Extended Property flags for GATT characteristics
   /// </summary>
   [Flags]
   public enum CharacteristicExtendedProperty
   {
      /// <summary>
      /// NotifyEncryptionRequired
      /// </summary>
      NotifyEncryptionRequired = 256,
      /// <summary>
      /// IndicateEncryptionRequired
      /// </summary>
      IndicateEncryptionRequired = 512
   }
}