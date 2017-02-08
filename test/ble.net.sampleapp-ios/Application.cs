// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using nexus.core;
using nexus.core.logging;
using UIKit;

namespace ble.net.sampleapp.ios
{
   public class Application
   {
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif

      internal static void Main( String[] args )
      {
         // ReSharper disable once ConditionIsAlwaysTrueOrFalse
         if(IS_DEBUG)
         {
            SystemLog.Instance.AddSink(
                        entry =>
                        {
                           var message = entry.FormatMessageAndArguments() + " " +
                                         entry.Data.Select( x => x?.ToString() + "" ).Join( " " );
                           if(entry.Severity == LogLevel.Error)
                           {
                              Console.Error.WriteLine( message );
                           }
                           else
                           {
                              Console.Out.WriteLine( message );
                           }
                        } );
         }

         UIApplication.Main( args, null, nameof( AppDelegate ) );
      }
   }
}