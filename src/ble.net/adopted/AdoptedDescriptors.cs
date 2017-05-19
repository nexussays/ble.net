// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.adopted
{
   /// <see href="https://www.bluetooth.com/specifications/gatt/descriptors" />
   public static class AdoptedDescriptors
   {
      /// <summary>
      /// Characteristic Extended Properties
      /// </summary>
      public static readonly IGattAttribute CharacteristicExtendedProperties =
         Desc( 0x2900, "Characteristic Extended Properties" );
      /// <summary>
      /// Characteristic User Description
      /// </summary>
      public static readonly IGattAttribute CharacteristicUserDescription =
         Desc( 0x2901, "Characteristic User Description" );
      /// <summary>
      /// Client characteristic configuration descriptor GUID
      /// </summary>
      /// <see
      ///    href="https://www.bluetooth.com/specifications/gatt/viewer?attributeXmlFile=org.bluetooth.descriptor.gatt.client_characteristic_configuration.xml" />
      public static readonly IGattAttribute ClientCharacteristicConfiguration =
         Desc( 0x2902, "Client Characteristic Configuration" );
      /// <summary>
      /// Server Characteristic Configuration
      /// </summary>
      public static readonly IGattAttribute ServerCharacteristicConfiguration =
         Desc( 0x2903, "Server Characteristic Configuration" );
      /// <summary>
      /// Characteristic Presentation Format
      /// </summary>
      public static readonly IGattAttribute CharacteristicFormat = Desc( 0x2904, "Characteristic Presentation Format" );
      /// <summary>
      /// Characteristic Aggregate Format
      /// </summary>
      public static readonly IGattAttribute CharacteristicAggregateFormat =
         Desc( 0x2905, "Characteristic Aggregate Format" );

      private static IGattAttribute Desc( UInt16 key, String name )
      {
         return new KnownAttribute( key.CreateGuidFromAdoptedKey(), name, GattAttributeType.Descriptor );
      }

      /// <inheritdoc cref="AddTo" />
      public static void AddAdoptedDescriptors( this KnownAttributes attributes )
      {
         AddTo( attributes );
      }

      /// <summary>
      /// Add the Bluetooth SIG adopted descriptors to <paramref name="attributes" />
      /// <remarks>
      ///    <see href="https://www.bluetooth.com/specifications/gatt/descriptors" />
      /// </remarks>
      /// </summary>
      public static void AddTo( KnownAttributes attributes )
      {
         attributes.Add( CharacteristicExtendedProperties );
         attributes.Add( CharacteristicUserDescription );
         attributes.Add( ClientCharacteristicConfiguration );
         attributes.Add( ServerCharacteristicConfiguration );
         attributes.Add( CharacteristicFormat );
         attributes.Add( CharacteristicAggregateFormat );
         attributes.AddDescriptor( 0x2906, "Valid Range" );
         attributes.AddDescriptor( 0x2907, "External Report Reference" );
         attributes.AddDescriptor( 0x2908, "Export Reference" );
      }
   }
}