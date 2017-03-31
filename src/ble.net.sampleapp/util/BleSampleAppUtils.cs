// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using nexus.core;

namespace ble.net.sampleapp.util
{
   internal static class BleSampleAppUtils
   {
      public enum BleCompanyId : ushort
      {
         Unknown = 0,
         Microsoft = 6,
         Apple = 76,
         Samsumg = 117,
         Google = 224,
         Xiaomi = 343
      }

      internal static String GetManufacturerName( Int32 key )
      {
         switch(key)
         {
            case 117:
               return "Samsung";
            case 224:
               return "Google";
            case 76:
               return "Apple";
            case 6:
               return "Microsoft";
            case 343:
               return "Xiaomi"; //"Anhui Huami";
            default:
               return key + "";
         }
      }

      internal static T ValueOr<T>( this Option<T> opt, T alt )
      {
         return opt.HasValue ? opt.Value : alt;
      }
   }
}