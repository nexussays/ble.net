// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

#pragma warning disable 1591

namespace nexus.protocols.ble
{
   public static class AssemblyInfo
   {
      public const String VERSION = "0.10.12";
      public const String VERSION_SHORT = VERSION;
      public const String ID = "ble.net";
      public const String URL = "https://github.com/nexussays/ble.net";
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif

      internal const String DESCRIPTION =
         "Cross-platform Bluetooth Low Energy (BLE) library for Android, iOS, and (partially) UWP";
   }
}