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
      public const Int32 COMPANY_ID_MICROSOFT = 6;
      public const Int32 COMPANY_ID_APPLE = 76;
      public const Int32 COMPANY_ID_SAMSUNG = 117;
      public const Int32 COMPANY_ID_GOOGLE = 224;
      public const Int32 COMPANY_ID_XIAOMI = 343;

      internal static String GetManufacturerName( Int32 key )
      {
         switch(key)
         {
            case COMPANY_ID_SAMSUNG: return "Samsung";
            case COMPANY_ID_GOOGLE: return "Google";
            case COMPANY_ID_APPLE: return "Apple";
            case COMPANY_ID_MICROSOFT: return "Microsoft";
            case COMPANY_ID_XIAOMI: return "Xiaomi"; //"Anhui Huami";
            default: return key + "";
         }
      }

      internal static T ValueOr<T>( this Option<T> opt, T alt )
      {
         return opt.HasValue ? opt.Value : alt;
      }
   }
}