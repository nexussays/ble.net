// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Globalization;
using Xamarin.Forms;

namespace ble.net.sampleapp.util
{
   public class InvertBooleanConverter : IValueConverter
   {
      public Object Convert( Object value, Type targetType, Object parameter, CultureInfo culture )
      {
         return !(Boolean)value;
      }

      public Object ConvertBack( Object value, Type targetType, Object parameter, CultureInfo culture )
      {
         return !(Boolean)value;
      }
   }
}
