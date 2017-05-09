// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.adopted
{
   /// <see href="https://www.bluetooth.com/specifications/gatt/descriptors" />
   public static class AdoptedDescriptors
   {
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
         attributes.AddDescriptor( 0x2900, "Characteristic Extended Properties" );
         attributes.AddDescriptor( 0x2901, "Characteristic User Description" );
         attributes.AddDescriptor( 0x2902, "Client Characteristic Configuration" );
         attributes.AddDescriptor( 0x2903, "Server Characteristic Configuration" );
         attributes.AddDescriptor( 0x2904, "Characteristic Presentation Format" );
         attributes.AddDescriptor( 0x2905, "Characteristic Aggregate Format" );
         attributes.AddDescriptor( 0x2906, "Valid Range" );
         attributes.AddDescriptor( 0x2907, "External Report Reference" );
         attributes.AddDescriptor( 0x2908, "Export Reference" );
      }
   }
}