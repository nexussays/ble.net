// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using System.Reflection;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;
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
         // ReSharper disable once InvertIf
         if(IS_DEBUG)
         {
            //
            // dump all entries to Android system log in debug mode
            //
            SystemLog.Instance.Id = Assembly.GetAssembly( GetType() ).GetName().Name;
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

   [Activity( Label = "BLE.net Sample App", Theme = "@style/MainTheme", MainLauncher = false, Icon = "@drawable/icon",
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
   public class MainActivity : FormsAppCompatActivity
   {
      protected override void OnActivityResult( Int32 requestCode, Result resultCode, Intent data )
      {
         BluetoothLowEnergyAdapter.OnActivityResult( requestCode, resultCode, data );
      }

      protected override void OnCreate( Bundle bundle )
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate( bundle );

         BluetoothLowEnergyAdapter.Init( this );
         UserDialogs.Init( this );
         Forms.Init( this, bundle );

         LoadApplication(
            new FormsApp( BluetoothLowEnergyAdapter.ObtainDefaultAdapter( ApplicationContext ), UserDialogs.Instance ) );
      }
   }

   [Activity(Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true)]
   public class SplashActivity : Activity
   {
      protected override void OnCreate( Bundle bundle )
      {
         base.OnCreate( bundle );
         StartActivity( typeof(MainActivity) );
         Finish();
      }
   }
}