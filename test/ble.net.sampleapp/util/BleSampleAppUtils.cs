using System;

namespace ble.net.sampleapp.util
{
   internal static class BleSampleAppUtils
   {
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
   }
}