// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.protocols.ble.gatt.adopted;

namespace ble.net.sampleapp.util
{
   /// <summary>
   /// Static instance of <see cref="KnownAttributes" /> for the application to use, with additional attributes added for a TI
   /// SensorTag. You can take a similar approach with your own custom BLE device & attributes.
   /// </summary>
   internal static class RegisteredAttributes
   {
      private const String DEVICE = "TI  SensorTag";
      private static readonly KnownAttributes s_known = new KnownAttributes();

      static RegisteredAttributes()
      {
         s_known.AddAdoptedCharacteristics();
         s_known.AddAdoptedDescriptors();
         s_known.AddAdoptedServices();

         s_known.AddService(
            new Guid( "00001530-1212-efde-1523-785feabcd123" ),
            "Nordic Device Firmware Update Service" );

         AddTiService( 0xaa00, "Infrared Thermometer" );
         AddTiService( 0xaa10, "Accelerometer" );
         AddTiService( 0xaa20, "Humidity" );
         AddTiService( 0xaa30, "Magnometer" );
         AddTiService( 0xaa40, "Barometer" );
         AddTiService( 0xaa50, "Gyroscope" );
         AddTiService( 0xaa60, "Test" );
         AddTiService( 0xccc0, "Connection Control" );
         AddTiService( 0xffc0, "OvertheAir Download" );

         AddTiChar( 0xaa01, "Infrared Temperature Data" );
         AddTiChar( 0xaa02, "Infrared Temperature On/Off" );
         AddTiChar( 0xaa03, "Infrared Temperature Sample Rate" );
         AddTiChar( 0xaa11, "Accelerometer Data" );
         AddTiChar( 0xaa12, "Accelerometer On/Off" );
         AddTiChar( 0xaa13, "Accelerometer Sample Rate" );
         AddTiChar( 0xaa21, "Humidity Data" );
         AddTiChar( 0xaa22, "Humidity On/Off" );
         AddTiChar( 0xaa23, "Humidity Sample Rate" );
         AddTiChar( 0xaa31, "Magnometer Data" );
         AddTiChar( 0xaa32, "Magnometer On/Off" );
         AddTiChar( 0xaa33, "Magnometer Sample Rate" );
         AddTiChar( 0xaa41, "Barometer Data" );
         AddTiChar( 0xaa42, "Barometer On/Off" );
         AddTiChar( 0xaa43, "Barometer Calibration" );
         AddTiChar( 0xaa44, "Barometer Sample Rate" );
         AddTiChar( 0xaa51, "Gyroscope Data" );
         AddTiChar( 0xaa52, "Gyroscope On/Off" );
         AddTiChar( 0xaa53, "Gyroscope Sample Rate" );
         AddTiChar( 0xaa61, "Test Data" );
         AddTiChar( 0xaa62, "Test Configuration" );
         AddTiChar( 0xccc1, "Connection Parameters" );
         AddTiChar( 0xccc2, "Connection Request Parameters" );
         AddTiChar( 0xccc3, "Connection Request Disconnect" );
         AddTiChar( 0xffc1, "OAD Image Identify" );
         AddTiChar( 0xffc2, "OAD Image Block" );
      }

      private static void AddTiChar( UInt16 key, String name )
      {
         s_known.AddCharacteristic( TiKey( key ), DEVICE + " " + name );
      }

      private static void AddTiService( UInt16 key, String name )
      {
         s_known.AddService( TiKey( key ), DEVICE + " " + name );
      }

      public static String GetName( Guid id )
      {
         return s_known.Get( id )?.Description;
      }

      public static Guid TiKey( this UInt16 key )
      {
         // see: http://processors.wiki.ti.com/index.php/SensorTag_User_Guide#Gatt_Server
         return new Guid( (Int32)(0xf0000000 | key), 0x0451, 0x4000, 0xb0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 );
      }
   }
}
