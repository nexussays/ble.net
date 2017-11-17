// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.gatt.adopted
{
   /// <summary>
   /// All services adopted by the BLE SIG. Instantiate a <see cref="KnownAttributes" /> in your application and add these
   /// services to it with <see cref="AddAdoptedServices" />
   /// </summary>
   /// <see href="https://www.bluetooth.com/specifications/gatt/services" />
   public static class AdoptedServices
   {
      /// <inheritdoc cref="AddTo" />
      public static void AddAdoptedServices( this KnownAttributes attributes )
      {
         AddTo( attributes );
      }

      /// <summary>
      /// Add the Bluetooth SIG adopted services to <paramref name="attributes" />.
      /// <remarks>
      ///    <see href="https://www.bluetooth.com/specifications/gatt/services" />
      /// </remarks>
      /// </summary>
      public static void AddTo( KnownAttributes attributes )
      {
         attributes.AddService( 0x1800, "Generic Access" );
         attributes.AddService( 0x1811, "Alert Notification Service" );
         attributes.AddService( 0x1815, "Automation IO" );
         attributes.AddService( 0x180F, "Battery Service" );
         attributes.AddService( 0x1810, "Blood Pressure" );
         attributes.AddService( 0x181B, "Body Composition" );
         attributes.AddService( 0x181E, "Bond Management Service" );
         attributes.AddService( 0x181F, "Continuous Glucose Monitoring" );
         attributes.AddService( 0x1805, "Current Time Service" );
         attributes.AddService( 0x1818, "Cycling Power" );
         attributes.AddService( 0x1816, "Cycling Speed and Cadence" );
         attributes.AddService( 0x180A, "Device Information" );
         attributes.AddService( 0x181A, "Environmental Sensing" );
         attributes.AddService( 0x1826, "Fitness Machine" );
         attributes.AddService( 0x1801, "Generic Attribute" );
         attributes.AddService( 0x1808, "Glucose" );
         attributes.AddService( 0x1809, "Health Thermometer" );
         attributes.AddService( 0x180D, "Heart Rate" );
         attributes.AddService( 0x1823, "HTTP Proxy" );
         attributes.AddService( 0x1812, "Human Interface Device" );
         attributes.AddService( 0x1802, "Immediate Alert" );
         attributes.AddService( 0x1821, "Indoor Positioning" );
         attributes.AddService( 0x1820, "Internet Protocol Support Service" );
         attributes.AddService( 0x1803, "Link Loss" );
         attributes.AddService( 0x1819, "Location and Navigation" );
         attributes.AddService( 0x1827, "Mesh Provisioning Service" );
         attributes.AddService( 0x1828, "Mesh Proxy Service" );
         attributes.AddService( 0x1807, "Next DST Change Service" );
         attributes.AddService( 0x1825, "Object Transfer Service" );
         attributes.AddService( 0x180E, "Phone Alert Status Service" );
         attributes.AddService( 0x1822, "Pulse Oximeter Service" );
         attributes.AddService( 0x1806, "Reference Time Update Service" );
         attributes.AddService( 0x1814, "Running Speed and Cadence" );
         attributes.AddService( 0x1813, "Scan Parameters" );
         attributes.AddService( 0x1824, "Transport Discovery" );
         attributes.AddService( 0x1804, "Tx Power" );
         attributes.AddService( 0x181C, "User Data" );
         attributes.AddService( 0x181D, "Weight Scale" );
      }
   }
}