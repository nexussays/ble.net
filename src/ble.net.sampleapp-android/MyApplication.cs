// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Reflection;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
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
            var logId = Assembly.GetAssembly( GetType() ).GetName().Name;
            SystemLog.Instance.AddSink(
               entry =>
               {
                  var message = entry.FormatAsString();
                  switch(entry.Severity)
                  {
                     case LogLevel.Error:
                        Log.Error( logId, message );
                        break;
                     case LogLevel.Warn:
                        Log.Warn( logId, message );
                        break;
                     case LogLevel.Info:
                        Log.Info( logId, message );
                        break;
                     case LogLevel.Trace:
                     default:
                        Log.Debug( logId, message );
                        break;
                  }
               } );
         }
      }
   }

   [Activity(
      Label = "BLE.net Sample App",
      Theme = "@style/MainTheme",
      MainLauncher = false,
      Icon = "@drawable/icon",
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
   public class MainActivity : FormsAppCompatActivity
   {
      /// <remarks>
      /// This must be implemented if you want to Subscribe() to IBluetoothLowEnergyAdapter.State to be notified when the
      /// bluetooth adapter state changes (i.e., it is enabled or disabled). If you don't care about that in your use-case, then
      /// you don't need to implement this -- you can still query the state of the adapter, the observable just won't work. See
      /// <see cref="IBluetoothLowEnergyAdapter.State" />
      /// </remarks>
      protected override void OnActivityResult( Int32 requestCode, Result resultCode, Intent data )
      {
         BluetoothLowEnergyAdapter.OnActivityResult( requestCode, resultCode, data );
      }

      protected override void OnCreate( Bundle bundle )
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate( bundle );

         UserDialogs.Init( this );
         Forms.Init( this, bundle );

         // If you want to enable/disable the Bluetooth adapter from code, you must call this.
         BluetoothLowEnergyAdapter.Init( this );
         // Obtain the bluetooth adapter so we can pass it into our (shared-code) Xamarin Forms app. There are
         // additional Obtain() methods on BluetoothLowEnergyAdapter if you have more specific needs (e.g. if you
         // need to support devices with multiple Bluetooth adapters)
         var bluetooth = BluetoothLowEnergyAdapter.ObtainDefaultAdapter( ApplicationContext );

         LoadApplication( new FormsApp( bluetooth, UserDialogs.Instance ) );
      }
   }

   [Activity( Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true )]
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
