// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.gatt.adopted
{
   /// <summary>
   /// All characteristics adopted by the BLE SIG. Instantiate a <see cref="KnownAttributes" /> in your application and add
   /// these characteristics to it with <see cref="AddAdoptedCharacteristics" />
   /// </summary>
   /// <see href="https://www.bluetooth.com/specifications/gatt/characteristics" />
   public static class AdoptedCharacteristics
   {
      /// <inheritdoc cref="AddTo" />
      public static void AddAdoptedCharacteristics( this KnownAttributes attributes )
      {
         AddTo( attributes );
      }

      /// <summary>
      /// Add the Bluetooth SIG adopted characteristics to <paramref name="attributes" />
      /// <remarks>
      ///    <see href="https://www.bluetooth.com/specifications/gatt/characteristics" />
      /// </remarks>
      /// </summary>
      public static void AddTo( KnownAttributes attributes )
      {
         attributes.AddCharacteristic( 0x2A7E, "Aerobic Heart Rate Lower Limit" );
         attributes.AddCharacteristic( 0x2A84, "Aerobic Heart Rate Upper Limit" );
         attributes.AddCharacteristic( 0x2A7F, "Aerobic Threshold" );
         attributes.AddCharacteristic( 0x2A80, "Age" );
         attributes.AddCharacteristic( 0x2A5A, "Aggregate" );
         attributes.AddCharacteristic( 0x2A43, "Alert Category ID" );
         attributes.AddCharacteristic( 0x2A42, "Alert Category ID Bit Mask" );
         attributes.AddCharacteristic( 0x2A06, "Alert Level" );
         attributes.AddCharacteristic( 0x2A44, "Alert Notification Control Point" );
         attributes.AddCharacteristic( 0x2A3F, "Alert Status" );
         attributes.AddCharacteristic( 0x2AB3, "Altitude" );
         attributes.AddCharacteristic( 0x2A81, "Anaerobic Heart Rate Lower Limit" );
         attributes.AddCharacteristic( 0x2A82, "Anaerobic Heart Rate Upper Limit" );
         attributes.AddCharacteristic( 0x2A83, "Anaerobic Threshold" );
         attributes.AddCharacteristic( 0x2A58, "Analog" );
         attributes.AddCharacteristic( 0x2A59, "Analog Output" );
         attributes.AddCharacteristic( 0x2A73, "Apparent Wind Direction" );
         attributes.AddCharacteristic( 0x2A72, "Apparent Wind Speed" );
         attributes.AddCharacteristic( 0x2A01, "Appearance" );
         attributes.AddCharacteristic( 0x2AA3, "Barometric Pressure Trend" );
         attributes.AddCharacteristic( 0x2A19, "Battery Level" );
         attributes.AddCharacteristic( 0x2A1B, "Battery Level State" );
         attributes.AddCharacteristic( 0x2A1A, "Battery Power State" );
         attributes.AddCharacteristic( 0x2A49, "Blood Pressure Feature" );
         attributes.AddCharacteristic( 0x2A35, "Blood Pressure Measurement" );
         attributes.AddCharacteristic( 0x2A9B, "Body Composition Feature" );
         attributes.AddCharacteristic( 0x2A9C, "Body Composition Measurement" );
         attributes.AddCharacteristic( 0x2A38, "Body Sensor Location" );
         attributes.AddCharacteristic( 0x2AA4, "Bond Management Control Point" );
         attributes.AddCharacteristic( 0x2AA5, "Bond Management Features" );
         attributes.AddCharacteristic( 0x2A22, "Boot Keyboard Input Report" );
         attributes.AddCharacteristic( 0x2A32, "Boot Keyboard Output Report" );
         attributes.AddCharacteristic( 0x2A33, "Boot Mouse Input Report" );
         attributes.AddCharacteristic( 0x2AA6, "Central Address Resolution" );
         attributes.AddCharacteristic( 0x2AA8, "CGM Feature" );
         attributes.AddCharacteristic( 0x2AA7, "CGM Measurement" );
         attributes.AddCharacteristic( 0x2AAB, "CGM Session Run Time" );
         attributes.AddCharacteristic( 0x2AAA, "CGM Session Start Time" );
         attributes.AddCharacteristic( 0x2AAC, "CGM Specific Ops Control Point" );
         attributes.AddCharacteristic( 0x2AA9, "CGM Status" );
         attributes.AddCharacteristic( 0x2ACE, "Cross Trainer Data" );
         attributes.AddCharacteristic( 0x2A5C, "CSC Feature" );
         attributes.AddCharacteristic( 0x2A5B, "CSC Measurement" );
         attributes.AddCharacteristic( 0x2A2B, "Current Time" );
         attributes.AddCharacteristic( 0x2A66, "Cycling Power Control Point" );
         attributes.AddCharacteristic( 0x2A65, "Cycling Power Feature" );
         attributes.AddCharacteristic( 0x2A63, "Cycling Power Measurement" );
         attributes.AddCharacteristic( 0x2A64, "Cycling Power Vector" );
         attributes.AddCharacteristic( 0x2A99, "Database Change Increment" );
         attributes.AddCharacteristic( 0x2A85, "Date of Birth" );
         attributes.AddCharacteristic( 0x2A86, "Date of Threshold Assessment" );
         attributes.AddCharacteristic( 0x2A08, "Date Time" );
         attributes.AddCharacteristic( 0x2A0A, "Day Date Time" );
         attributes.AddCharacteristic( 0x2A09, "Day of Week" );
         attributes.AddCharacteristic( 0x2A7D, "Descriptor Value Changed" );
         attributes.AddCharacteristic( 0x2A00, "Device Name" );
         attributes.AddCharacteristic( 0x2A7B, "Dew Point" );
         attributes.AddCharacteristic( 0x2A56, "Digital" );
         attributes.AddCharacteristic( 0x2A57, "Digital Output" );
         attributes.AddCharacteristic( 0x2A0D, "DST Offset" );
         attributes.AddCharacteristic( 0x2A6C, "Elevation" );
         attributes.AddCharacteristic( 0x2A87, "Email Address" );
         attributes.AddCharacteristic( 0x2A0B, "Exact Time 100" );
         attributes.AddCharacteristic( 0x2A0C, "Exact Time 256" );
         attributes.AddCharacteristic( 0x2A88, "Fat Burn Heart Rate Lower Limit" );
         attributes.AddCharacteristic( 0x2A89, "Fat Burn Heart Rate Upper Limit" );
         attributes.AddCharacteristic( 0x2A26, "Firmware Revision String" );
         attributes.AddCharacteristic( 0x2A8A, "First Name" );
         attributes.AddCharacteristic( 0x2AD9, "Fitness Machine Control Point" );
         attributes.AddCharacteristic( 0x2ACC, "Fitness Machine Feature" );
         attributes.AddCharacteristic( 0x2ADA, "Fitness Machine Status" );
         attributes.AddCharacteristic( 0x2A8B, "Five Zone Heart Rate Limits" );
         attributes.AddCharacteristic( 0x2AB2, "Floor Number" );
         attributes.AddCharacteristic( 0x2A8C, "Gender" );
         attributes.AddCharacteristic( 0x2A51, "Glucose Feature" );
         attributes.AddCharacteristic( 0x2A18, "Glucose Measurement" );
         attributes.AddCharacteristic( 0x2A34, "Glucose Measurement Context" );
         attributes.AddCharacteristic( 0x2A74, "Gust Factor" );
         attributes.AddCharacteristic( 0x2A27, "Hardware Revision String" );
         attributes.AddCharacteristic( 0x2A39, "Heart Rate Control Point" );
         attributes.AddCharacteristic( 0x2A8D, "Heart Rate Max" );
         attributes.AddCharacteristic( 0x2A37, "Heart Rate Measurement" );
         attributes.AddCharacteristic( 0x2A7A, "Heat Index" );
         attributes.AddCharacteristic( 0x2A8E, "Height" );
         attributes.AddCharacteristic( 0x2A4C, "HID Control Point" );
         attributes.AddCharacteristic( 0x2A4A, "HID Information" );
         attributes.AddCharacteristic( 0x2A8F, "Hip Circumference" );
         attributes.AddCharacteristic( 0x2ABA, "HTTP Control Point" );
         attributes.AddCharacteristic( 0x2AB9, "HTTP Entity Body" );
         attributes.AddCharacteristic( 0x2AB7, "HTTP Headers" );
         attributes.AddCharacteristic( 0x2AB8, "HTTP Status Code" );
         attributes.AddCharacteristic( 0x2ABB, "HTTPS Security" );
         attributes.AddCharacteristic( 0x2A6F, "Humidity" );
         attributes.AddCharacteristic( 0x2A2A, "IEEE 11073-20601 Regulatory Certification Data List" );
         attributes.AddCharacteristic( 0x2AD2, "Indoor Bike Data" );
         attributes.AddCharacteristic( 0x2AAD, "Indoor Positioning Configuration" );
         attributes.AddCharacteristic( 0x2A36, "Intermediate Cuff Pressure" );
         attributes.AddCharacteristic( 0x2A1E, "Intermediate Temperature" );
         attributes.AddCharacteristic( 0x2A77, "Irradiance" );
         attributes.AddCharacteristic( 0x2AA2, "Language" );
         attributes.AddCharacteristic( 0x2A90, "Last Name" );
         attributes.AddCharacteristic( 0x2AAE, "Latitude" );
         attributes.AddCharacteristic( 0x2A6B, "LN Control Point" );
         attributes.AddCharacteristic( 0x2A6A, "LN Feature" );
         attributes.AddCharacteristic( 0x2AB1, "Local East Coordinate" );
         attributes.AddCharacteristic( 0x2AB0, "Local North Coordinate" );
         attributes.AddCharacteristic( 0x2A0F, "Local Time Information" );
         attributes.AddCharacteristic( 0x2A67, "Location and Speed Characteristic" );
         attributes.AddCharacteristic( 0x2AB5, "Location Name" );
         attributes.AddCharacteristic( 0x2AAF, "Longitude" );
         attributes.AddCharacteristic( 0x2A2C, "Magnetic Declination" );
         attributes.AddCharacteristic( 0x2AA0, "Magnetic Flux Density - 2D" );
         attributes.AddCharacteristic( 0x2AA1, "Magnetic Flux Density - 3D" );
         attributes.AddCharacteristic( 0x2A29, "Manufacturer Name String" );
         attributes.AddCharacteristic( 0x2A91, "Maximum Recommended Heart Rate" );
         attributes.AddCharacteristic( 0x2A21, "Measurement Interval" );
         attributes.AddCharacteristic( 0x2A24, "Model Number String" );
         attributes.AddCharacteristic( 0x2A68, "Navigation" );
         attributes.AddCharacteristic( 0x2A3E, "Network Availability" );
         attributes.AddCharacteristic( 0x2A46, "New Alert" );
         attributes.AddCharacteristic( 0x2AC5, "Object Action Control Point" );
         attributes.AddCharacteristic( 0x2AC8, "Object Changed" );
         attributes.AddCharacteristic( 0x2AC1, "Object First-Created" );
         attributes.AddCharacteristic( 0x2AC3, "Object ID" );
         attributes.AddCharacteristic( 0x2AC2, "Object Last-Modified" );
         attributes.AddCharacteristic( 0x2AC6, "Object List Control Point" );
         attributes.AddCharacteristic( 0x2AC7, "Object List Filter" );
         attributes.AddCharacteristic( 0x2ABE, "Object Name" );
         attributes.AddCharacteristic( 0x2AC4, "Object Properties" );
         attributes.AddCharacteristic( 0x2AC0, "Object Size" );
         attributes.AddCharacteristic( 0x2ABF, "Object Type" );
         attributes.AddCharacteristic( 0x2ABD, "OTS Feature" );
         attributes.AddCharacteristic( 0x2A04, "Peripheral Preferred Connection Parameters" );
         attributes.AddCharacteristic( 0x2A02, "Peripheral Privacy Flag" );
         attributes.AddCharacteristic( 0x2A5F, "PLX Continuous Measurement Characteristic" );
         attributes.AddCharacteristic( 0x2A5E, "PLX Spot-Check Measurement" );
         attributes.AddCharacteristic( 0x2A50, "PnP ID" );
         attributes.AddCharacteristic( 0x2A75, "Pollen Concentration" );
         attributes.AddCharacteristic( 0x2A2F, "Position 2D" );
         attributes.AddCharacteristic( 0x2A30, "Position 3D" );
         attributes.AddCharacteristic( 0x2A69, "Position Quality" );
         attributes.AddCharacteristic( 0x2A6D, "Pressure" );
         attributes.AddCharacteristic( 0x2A4E, "Protocol Mode" );
         attributes.AddCharacteristic( 0x2A62, "Pulse Oximetry Control Point" );
         attributes.AddCharacteristic( 0x2A60, "Pulse Oximetry Pulsatile Event Characteristic" );
         attributes.AddCharacteristic( 0x2A78, "Rainfall" );
         attributes.AddCharacteristic( 0x2A03, "Reconnection Address" );
         attributes.AddCharacteristic( 0x2A52, "Record Access Control Point" );
         attributes.AddCharacteristic( 0x2A14, "Reference Time Information" );
         attributes.AddCharacteristic( 0x2A3A, "Removable" );
         attributes.AddCharacteristic( 0x2A4D, "Report" );
         attributes.AddCharacteristic( 0x2A4B, "Report Map" );
         attributes.AddCharacteristic( 0x2AC9, "Resolvable Private Address Only" );
         attributes.AddCharacteristic( 0x2A92, "Resting Heart Rate" );
         attributes.AddCharacteristic( 0x2A40, "Ringer Control point" );
         attributes.AddCharacteristic( 0x2A41, "Ringer Setting" );
         attributes.AddCharacteristic( 0x2AD1, "Rower Data" );
         attributes.AddCharacteristic( 0x2A54, "RSC Feature" );
         attributes.AddCharacteristic( 0x2A53, "RSC Measurement" );
         attributes.AddCharacteristic( 0x2A55, "SC Control Point" );
         attributes.AddCharacteristic( 0x2A4F, "Scan Interval Window" );
         attributes.AddCharacteristic( 0x2A31, "Scan Refresh" );
         attributes.AddCharacteristic( 0x2A3C, "Scientific Temperature Celsius" );
         attributes.AddCharacteristic( 0x2A10, "Secondary Time Zone" );
         attributes.AddCharacteristic( 0x2A5D, "Sensor Location" );
         attributes.AddCharacteristic( 0x2A25, "Serial Number String" );
         attributes.AddCharacteristic( 0x2A05, "Service Changed" );
         attributes.AddCharacteristic( 0x2A3B, "Service Required" );
         attributes.AddCharacteristic( 0x2A28, "Software Revision String" );
         attributes.AddCharacteristic( 0x2A93, "Sport Type for Aerobic and Anaerobic Thresholds" );
         attributes.AddCharacteristic( 0x2AD0, "Stair Climber Data" );
         attributes.AddCharacteristic( 0x2ACF, "Step Climber Data" );
         attributes.AddCharacteristic( 0x2A3D, "String" );
         attributes.AddCharacteristic( 0x2AD7, "Supported Heart Rate Range" );
         attributes.AddCharacteristic( 0x2AD5, "Supported Inclination Range" );
         attributes.AddCharacteristic( 0x2A47, "Supported New Alert Category" );
         attributes.AddCharacteristic( 0x2AD8, "Supported Power Range" );
         attributes.AddCharacteristic( 0x2AD6, "Supported Resistance Level Range" );
         attributes.AddCharacteristic( 0x2AD4, "Supported Speed Range" );
         attributes.AddCharacteristic( 0x2A48, "Supported Unread Alert Category" );
         attributes.AddCharacteristic( 0x2A23, "System ID" );
         attributes.AddCharacteristic( 0x2ABC, "TDS Control Point" );
         attributes.AddCharacteristic( 0x2A6E, "Temperature" );
         attributes.AddCharacteristic( 0x2A1F, "Temperature Celsius" );
         attributes.AddCharacteristic( 0x2A20, "Temperature Fahrenheit" );
         attributes.AddCharacteristic( 0x2A1C, "Temperature Measurement" );
         attributes.AddCharacteristic( 0x2A1D, "Temperature Type" );
         attributes.AddCharacteristic( 0x2A94, "Three Zone Heart Rate Limits" );
         attributes.AddCharacteristic( 0x2A12, "Time Accuracy" );
         attributes.AddCharacteristic( 0x2A15, "Time Broadcast" );
         attributes.AddCharacteristic( 0x2A13, "Time Source" );
         attributes.AddCharacteristic( 0x2A16, "Time Update Control Point" );
         attributes.AddCharacteristic( 0x2A17, "Time Update State" );
         attributes.AddCharacteristic( 0x2A11, "Time with DST" );
         attributes.AddCharacteristic( 0x2A0E, "Time Zone" );
         attributes.AddCharacteristic( 0x2AD3, "Training Status" );
         attributes.AddCharacteristic( 0x2ACD, "Treadmill Data" );
         attributes.AddCharacteristic( 0x2A71, "True Wind Direction" );
         attributes.AddCharacteristic( 0x2A70, "True Wind Speed" );
         attributes.AddCharacteristic( 0x2A95, "Two Zone Heart Rate Limit" );
         attributes.AddCharacteristic( 0x2A07, "Tx Power Level" );
         attributes.AddCharacteristic( 0x2AB4, "Uncertainty" );
         attributes.AddCharacteristic( 0x2A45, "Unread Alert Status" );
         attributes.AddCharacteristic( 0x2AB6, "URI" );
         attributes.AddCharacteristic( 0x2A9F, "User Control Point" );
         attributes.AddCharacteristic( 0x2A9A, "User Index" );
         attributes.AddCharacteristic( 0x2A76, "UV Index" );
         attributes.AddCharacteristic( 0x2A96, "VO2 Max" );
         attributes.AddCharacteristic( 0x2A97, "Waist Circumference" );
         attributes.AddCharacteristic( 0x2A98, "Weight" );
         attributes.AddCharacteristic( 0x2A9D, "Weight Measurement" );
         attributes.AddCharacteristic( 0x2A9E, "Weight Scale Feature" );
         attributes.AddCharacteristic( 0x2A79, "Wind Chill" );
      }
   }
}