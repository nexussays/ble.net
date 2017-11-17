// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble.gatt.adopted
{
   /// <summary>
   /// All descriptors adopted by the BLE SIG. Instantiate a <see cref="KnownAttributes" /> in your application and add these
   /// descriptors to it with <see cref="AddAdoptedDescriptors" />
   /// </summary>
   /// <see href="https://www.bluetooth.com/specifications/gatt/descriptors" />
   public static class AdoptedDescriptors
   {
      /// <summary>
      /// Characteristic Extended Properties
      /// </summary>
      public static readonly IGattAttribute CharacteristicExtendedProperties = Desc(
         0x2900,
         "Characteristic Extended Properties" );
      /// <summary>
      /// Characteristic User Description
      /// </summary>
      public static readonly IGattAttribute CharacteristicUserDescription = Desc(
         0x2901,
         "Characteristic User Description" );
      /// <summary>
      /// Client characteristic configuration descriptor GUID
      /// </summary>
      /// <see
      ///    href="https://www.bluetooth.com/specifications/gatt/viewer?attributeXmlFile=org.bluetooth.descriptor.gatt.client_characteristic_configuration.xml" />
      public static readonly IGattAttribute ClientCharacteristicConfiguration = Desc(
         0x2902,
         "Client Characteristic Configuration" );
      /// <summary>
      /// Server Characteristic Configuration
      /// </summary>
      public static readonly IGattAttribute ServerCharacteristicConfiguration = Desc(
         0x2903,
         "Server Characteristic Configuration" );
      /// <summary>
      /// Characteristic Presentation Format
      /// </summary>
      public static readonly IGattAttribute CharacteristicPresentationFormat = Desc(
         0x2904,
         "Characteristic Presentation Format" );
      /// <summary>
      /// Characteristic Aggregate Format
      /// </summary>
      public static readonly IGattAttribute CharacteristicAggregateFormat = Desc(
         0x2905,
         "Characteristic Aggregate Format" );

      private static IGattAttribute Desc( UInt16 key, String name )
      {
         return new GattAttribute( GattAttributeType.Descriptor, key.CreateGuidFromAdoptedKey(), name );
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
         attributes.Add( CharacteristicPresentationFormat );
         attributes.Add( CharacteristicAggregateFormat );

         attributes.AddDescriptor( 0x2906, "Valid Range" );
         attributes.AddDescriptor( 0x2907, "External Report Reference" );
         attributes.AddDescriptor( 0x2908, "Export Reference" );
         attributes.AddDescriptor( 0x290B, "Environmental Sensing Configuration" );
         attributes.AddDescriptor( 0x290C, "Environmental Sensing Measurement" );
         attributes.AddDescriptor( 0x290D, "Environmental Sensing Trigger Setting" );
         attributes.AddDescriptor( 0x2909, "Number of Digitals" );
         attributes.AddDescriptor( 0x290E, "Time Trigger Setting" );
         attributes.AddDescriptor( 0x290A, "Value Trigger Setting" );
      }
   }
}