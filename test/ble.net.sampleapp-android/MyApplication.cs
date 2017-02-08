// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using Android.App;
using Android.Runtime;
using nexus.core;
using nexus.core.logging;
using Log = Android.Util.Log;

namespace ble.net.sampleapp.android
{
   [Application( Debuggable = IS_DEBUG, AllowBackup = true, AllowClearUserData = true )]
   public class MyApplication : Application
   {
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif

      protected MyApplication( IntPtr javaReference, JniHandleOwnership transfer )
         : base( javaReference, transfer )
      {
      }

      public override void OnCreate()
      {
         // ReSharper disable once ConditionIsAlwaysTrueOrFalse
         if(IS_DEBUG)
         {
            // dump all entries to Android system log in debug mode
            SystemLog.Instance.AddSink(
                        entry =>
                        {
                           var message = entry.FormatMessageAndArguments() + " " +
                                         entry.Data.Select( x => x?.ToString() + "" ).Join( " " );
                           switch(entry.Severity)
                           {
                              case LogLevel.Error:

                                 Log.Error( entry.LogId, message );
                                 break;
                              case LogLevel.Warn:
                                 Log.Warn( entry.LogId, message );
                                 break;
                              case LogLevel.Info:
                                 Log.Info( entry.LogId, message );
                                 break;
                              default:
                                 Log.Debug( entry.LogId, message );
                                 break;
                           }
                        } );
         }
      }
   }
}