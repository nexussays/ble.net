// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble.adopted
{
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
         attributes.AddCharacteristic( 0x2a43, "Alert Category ID" );
         attributes.AddCharacteristic( 0x2a42, "Alert Category ID Bit Mask" );
         attributes.AddCharacteristic( 0x2a06, "Alert Level" );
         attributes.AddCharacteristic( 0x2a44, "Alert Notification Control Point" );
         attributes.AddCharacteristic( 0x2a3f, "Alert Status" );
         attributes.AddCharacteristic( 0x2a01, "Appearance" );
         attributes.AddCharacteristic( 0x2a19, "Battery Level" );
         attributes.AddCharacteristic( 0x2a49, "Blood Pressure Feature" );
         attributes.AddCharacteristic( 0x2a35, "Blood Pressure Measurement" );
         attributes.AddCharacteristic( 0x2a38, "Body Sensor Location" );
         attributes.AddCharacteristic( 0x2a22, "Boot Keyboard Input Report" );
         attributes.AddCharacteristic( 0x2a32, "Boot Keyboard Output Report" );
         attributes.AddCharacteristic( 0x2a33, "Boot Mouse Input Report" );
         attributes.AddCharacteristic( 0x2a5c, "CSC Feature" );
         attributes.AddCharacteristic( 0x2a5b, "CSC Measurement" );
         attributes.AddCharacteristic( 0x2a2b, "Current Time" );
         attributes.AddCharacteristic( 0x2a66, "Cycling Power Control Point" );
         attributes.AddCharacteristic( 0x2a65, "Cycling Power Feature" );
         attributes.AddCharacteristic( 0x2a63, "Cycling Power Measurement" );
         attributes.AddCharacteristic( 0x2a64, "Cycling Power Vector" );
         attributes.AddCharacteristic( 0x2a08, "Date Time" );
         attributes.AddCharacteristic( 0x2a0a, "Day Date Time" );
         attributes.AddCharacteristic( 0x2a09, "Day of Week" );
         attributes.AddCharacteristic( 0x2a00, "Device Name" );
         attributes.AddCharacteristic( 0x2a0d, "DST Offset" );
         attributes.AddCharacteristic( 0x2a0c, "Exact Time 256" );
         attributes.AddCharacteristic( 0x2a26, "Firmware Revision String" );
         attributes.AddCharacteristic( 0x2a51, "Glucose Feature" );
         attributes.AddCharacteristic( 0x2a18, "Glucose Measurement" );
         attributes.AddCharacteristic( 0x2a34, "Glucose Measure Context" );
         attributes.AddCharacteristic( 0x2a27, "Hardware Revision String" );
         attributes.AddCharacteristic( 0x2a39, "Heart Rate Control Point" );
         attributes.AddCharacteristic( 0x2a37, "Heart Rate Measurement" );
         attributes.AddCharacteristic( 0x2a4c, "HID Control Point" );
         attributes.AddCharacteristic( 0x2a4a, "HID Information" );
         attributes.AddCharacteristic( 0x2a36, "Intermediate Cuff Pressure" );
         attributes.AddCharacteristic( 0x2a1e, "Intermediate Temperature" );
         attributes.AddCharacteristic( 0x2a6b, "LN Control Point" );
         attributes.AddCharacteristic( 0x2a6a, "LN Feature" );
         attributes.AddCharacteristic( 0x2a0f, "Local Time Information" );
         attributes.AddCharacteristic( 0x2a67, "Location And Speed" );
         attributes.AddCharacteristic( 0x2a29, "Manufacturer Name String" );
         attributes.AddCharacteristic( 0x2a21, "Measurement Interval" );
         attributes.AddCharacteristic( 0x2a24, "Model Number String" );
         attributes.AddCharacteristic( 0x2a68, "Navigation" );
         attributes.AddCharacteristic( 0x2a46, "New Alert" );
         attributes.AddCharacteristic( 0x2a04, "Peripheral Preferred Connection Parameters" );
         attributes.AddCharacteristic( 0x2a02, "Peripheral Privacy Flag" );
         attributes.AddCharacteristic( 0x2a50, "PnP ID" );
         attributes.AddCharacteristic( 0x2a69, "Position Quality" );
         attributes.AddCharacteristic( 0x2a4e, "Protocol Mode" );
         attributes.AddCharacteristic( 0x2a03, "Reconnection Address" );
         attributes.AddCharacteristic( 0x2a52, "Record Access Control Point" );
         attributes.AddCharacteristic( 0x2a14, "Reference Time Information" );
         attributes.AddCharacteristic( 0x2a4d, "Report" );
         attributes.AddCharacteristic( 0x2a4b, "Report Map" );
         attributes.AddCharacteristic( 0x2a40, "Ringer Control Point" );
         attributes.AddCharacteristic( 0x2a41, "Ringer Setting" );
         attributes.AddCharacteristic( 0x2a54, "RSC Feature" );
         attributes.AddCharacteristic( 0x2a53, "RSC Measurement" );
         attributes.AddCharacteristic( 0x2a55, "SC Control Point" );
         attributes.AddCharacteristic( 0x2a4f, "Scan Interval Window" );
         attributes.AddCharacteristic( 0x2a31, "Scan Refresh" );
         attributes.AddCharacteristic( 0x2a5d, "Sensor Location" );
         attributes.AddCharacteristic( 0x2a25, "Serial Number String" );
         attributes.AddCharacteristic( 0x2a05, "Service Changed" );
         attributes.AddCharacteristic( 0x2a28, "Software Revision String" );
         attributes.AddCharacteristic( 0x2a47, "Supported New Alert Category" );
         attributes.AddCharacteristic( 0x2a48, "Supported Unread Alert Category" );
         attributes.AddCharacteristic( 0x2a23, "System ID" );
         attributes.AddCharacteristic( 0x2a1c, "Temperature Measurement" );
         attributes.AddCharacteristic( 0x2a1d, "Temperature Type" );
         attributes.AddCharacteristic( 0x2a12, "Time Accuracy" );
         attributes.AddCharacteristic( 0x2a13, "Time Source" );
         attributes.AddCharacteristic( 0x2a16, "Time Update Control Point" );
         attributes.AddCharacteristic( 0x2a17, "Time Update State" );
         attributes.AddCharacteristic( 0x2a11, "Time with DST" );
         attributes.AddCharacteristic( 0x2a0e, "Time Zone" );
         attributes.AddCharacteristic( 0x2a07, "Tx Power Level" );
         attributes.AddCharacteristic( 0x2a45, "Unread Alert Status" );
         attributes.AddCharacteristic( 0x2a7e, "Aerobic Heart Rate Lower Limit" );
         attributes.AddCharacteristic( 0x2a84, "Aerobic Heart Rate Uppoer Limit" );
         attributes.AddCharacteristic( 0x2a80, "Age" );
         attributes.AddCharacteristic( 0x2a5a, "Aggregate Input" );
         attributes.AddCharacteristic( 0x2a81, "Anaerobic Heart Rate Lower Limit" );
         attributes.AddCharacteristic( 0x2a82, "Anaerobic Heart Rate Upper Limit" );
         attributes.AddCharacteristic( 0x2a83, "Anaerobic Threshold" );
         attributes.AddCharacteristic( 0x2a58, "Analog" );
         attributes.AddCharacteristic( 0x2a73, "Apparent Wind Direction" );
         attributes.AddCharacteristic( 0x2a72, "Apparent Wind Speed" );
         attributes.AddCharacteristic( 0x2a9b, "Body Composition Feature" );
         attributes.AddCharacteristic( 0x2a9c, "Body Composition Measurement" );
         attributes.AddCharacteristic( 0x2a99, "Database Change Increment" );
         attributes.AddCharacteristic( 0x2a85, "Date of Birth" );
         attributes.AddCharacteristic( 0x2a86, "Date of Threshold Assessment" );
         attributes.AddCharacteristic( 0x2a7d, "Descriptor Value Changed" );
         attributes.AddCharacteristic( 0x2a7b, "Dew Point" );
         attributes.AddCharacteristic( 0x2a56, "Digital Input" );
         attributes.AddCharacteristic( 0x2a57, "Digital Output" );
         attributes.AddCharacteristic( 0x2a6c, "Elevation" );
         attributes.AddCharacteristic( 0x2a87, "Email Address" );
         attributes.AddCharacteristic( 0x2a0b, "Exact Time 100" );
         attributes.AddCharacteristic( 0x2a88, "Fat Burn Heart Rate Lower Limit" );
         attributes.AddCharacteristic( 0x2a89, "Fat Burn Heart Rate Upper Limit" );
         attributes.AddCharacteristic( 0x2a8a, "First Name" );
         attributes.AddCharacteristic( 0x2a8b, "Five Zone Heart Rate Limits" );
         attributes.AddCharacteristic( 0x2a8c, "Gender" );
         attributes.AddCharacteristic( 0x2a74, "Gust Factor" );
         attributes.AddCharacteristic( 0x2a8d, "Heart Rate Max" );
         attributes.AddCharacteristic( 0x2a7a, "Heat Index" );
         attributes.AddCharacteristic( 0x2a8e, "Height" );
         attributes.AddCharacteristic( 0x2a8f, "Hip Circumference" );
         attributes.AddCharacteristic( 0x2a6f, "Humidity" );
         attributes.AddCharacteristic( 0x2a77, "Irradiance" );
         attributes.AddCharacteristic( 0x2a90, "Last Name" );
         attributes.AddCharacteristic( 0x2a91, "Maximum Recommended Heart Rate" );
         attributes.AddCharacteristic( 0x2a3e, "Network Availability" );
         attributes.AddCharacteristic( 0x2a75, "Pollen Concentration" );
         attributes.AddCharacteristic( 0x2a6d, "Pressure" );
         attributes.AddCharacteristic( 0x2a78, "Rainfall" );
         attributes.AddCharacteristic( 0x2a92, "Resting Heart Rate" );
         attributes.AddCharacteristic( 0x2a3c, "Scientific Temperature in Celsius" );
         attributes.AddCharacteristic( 0x2a10, "Secondary Time Zone" );
         attributes.AddCharacteristic( 0x2a93, "Sport Type for Aerobic and Anaerobic Thresholds" );
         attributes.AddCharacteristic( 0x2a3d, "String" );
         attributes.AddCharacteristic( 0x2a6e, "Temperature" );
         attributes.AddCharacteristic( 0x2a1f, "Temperature in Celsius" );
         attributes.AddCharacteristic( 0x2a20, "Temperature in Fahrenheit" );
         attributes.AddCharacteristic( 0x2a94, "Three Zone Heart Rate Limits" );
         attributes.AddCharacteristic( 0x2a15, "Time Broadcast" );
         attributes.AddCharacteristic( 0x2a7c, "Trend" );
         attributes.AddCharacteristic( 0x2a71, "True Wind Direction" );
         attributes.AddCharacteristic( 0x2a70, "True Wind Speed" );
         attributes.AddCharacteristic( 0x2a95, "Two Zone Heart Rate Limit" );
         attributes.AddCharacteristic( 0x2a9f, "User Control Point" );
         attributes.AddCharacteristic( 0x2a9a, "User Index" );
         attributes.AddCharacteristic( 0x2a76, "UV Index" );
         attributes.AddCharacteristic( 0x2a96, "VO2 Max" );
         attributes.AddCharacteristic( 0x2a97, "Waist Circumference" );
         attributes.AddCharacteristic( 0x2a98, "Weight" );
         attributes.AddCharacteristic( 0x2a9d, "Weight Measurement" );
         attributes.AddCharacteristic( 0x2a9e, "Weight Scale Feature" );
         attributes.AddCharacteristic( 0x2a79, "Wind Chill" );
         attributes.AddCharacteristic( 0x2a1b, "Battery Level State" );
         attributes.AddCharacteristic( 0x2a1a, "Battery Power State" );
         attributes.AddCharacteristic( 0x2a2d, "Latitude" );
         attributes.AddCharacteristic( 0x2a2e, "Longitude" );
         attributes.AddCharacteristic( 0x2a2f, "Position 2D" );
         attributes.AddCharacteristic( 0x2a30, "Position 3D" );
         attributes.AddCharacteristic( 0x2a5f, "Pulse Oximetry Continuous Measurement" );
         attributes.AddCharacteristic( 0x2a62, "Pulse Oximetry Control Point" );
         attributes.AddCharacteristic( 0x2a61, "Pulse Oximetry Features" );
         attributes.AddCharacteristic( 0x2a60, "Pulse Oximetry Pulsatile Event" );
         attributes.AddCharacteristic( 0x2a5e, "Pulse Oximetry Spot-Check Measurement" );
         attributes.AddCharacteristic( 0x2a3a, "Removable" );
         attributes.AddCharacteristic( 0x2a3b, "Service Required" );
      }
   }
}