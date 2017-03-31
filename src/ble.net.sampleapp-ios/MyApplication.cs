// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using System.Reflection;
using Acr.UserDialogs;
using Foundation;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ble.net.sampleapp.ios
{
   public class MyApplication
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
#pragma warning disable 162
            SystemLog.Instance.Id = Assembly.GetAssembly( typeof(MyApplication) ).GetName().Name;
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
#pragma warning restore 162
         }

         UIApplication.Main( args, null, nameof( AppDelegate ) );
      }
   }

   [Register( "AppDelegate" )]
   public class AppDelegate : FormsApplicationDelegate
   {
      public override Boolean FinishedLaunching( UIApplication app, NSDictionary options )
      {
         Forms.Init();
         LoadApplication( new FormsApp( BluetoothLowEnergyAdapter.ObtainDefaultAdapter(), UserDialogs.Instance ) );
         return base.FinishedLaunching( app, options );
      }
   }
}