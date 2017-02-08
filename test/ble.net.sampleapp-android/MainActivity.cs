// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ble.net.sampleapp.android
{
   [Activity( Theme = "@style/MainTheme", MainLauncher = true, NoHistory = true )]
   public class SplashActivity : Activity
   {
      protected override void OnCreate( Bundle bundle )
      {
         base.OnCreate( bundle );
         StartActivity( typeof(MainActivity) );
         Finish();
      }
   }

   [Activity( Label = "BLE.net Sample App", Theme = "@style/MainTheme", MainLauncher = false,
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
}