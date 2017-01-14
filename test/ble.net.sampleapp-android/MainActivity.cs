using System;
using System.Linq;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Java.Util;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ble.net.sampleapp.android
{
   [Activity( Label = "ble.net.sampleapp", Theme = "@style/MainTheme", MainLauncher = true,
       ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
   public class MainActivity : FormsAppCompatActivity
   {
      public override void OnRequestPermissionsResult( Int32 requestCode, String[] permissions )
      {
         Log.Info( "OnRequestPermissionsResult: code={0} permissions={1}", requestCode, permissions.Join( ", " ) );
      }

      public override void OnRequestPermissionsResult( Int32 requestCode, String[] permissions,
                                                       Permission[] grantResults )
      {
         Log.Info(
            "OnRequestPermissionsResult: code={0} permissions={1} grants={2}",
            requestCode,
            permissions.Join( ", " ),
            grantResults.Select( x => x.ToString() ).Join( ", " ) );
      }

      protected override void OnActivityResult( Int32 requestCode, Result resultCode, Intent data )
      {
         BluetoothLowEnergyAdapter.OnActivityResult( requestCode, resultCode, data );
      }

      protected override void OnCreate( Bundle bundle )
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate( bundle );

         BluetoothLowEnergyAdapter.InitActivity( this );
         UserDialogs.Init( this );
         Forms.Init( this, bundle );

         new Timer().Schedule(
            new TimerAction(
               () =>
               {
                  RunOnUiThread(
                     () =>
                     {
                        if(ContextCompat.CheckSelfPermission( this, Manifest.Permission.Bluetooth ) !=
                           Permission.Granted)
                        {
                           if(ActivityCompat.ShouldShowRequestPermissionRationale( this, Manifest.Permission.Bluetooth ))
                           {
                              UserDialogs.Instance.Alert(
                                            "Yes should show rationale for Bluetooth",
                                            "ShouldShowRequestPermissionRationale" );
                           }
                           else
                           {
                              Log.Info( "Requesting permission for Bluetooth" );
                              ActivityCompat.RequestPermissions( this, new[] {Manifest.Permission.Bluetooth}, 24112 );
                           }
                        }
                        else
                        {
                           Log.Info( "Already have permission for Bluetooth" );
                        }

                        if(ContextCompat.CheckSelfPermission( this, Manifest.Permission.BluetoothAdmin ) !=
                           Permission.Granted)
                        {
                           if(ActivityCompat.ShouldShowRequestPermissionRationale(
                              this,
                              Manifest.Permission.BluetoothAdmin ))
                           {
                              UserDialogs.Instance.Alert(
                                            "Yes should show rationale for BluetoothAdmin",
                                            "ShouldShowRequestPermissionRationale" );
                           }
                           else
                           {
                              Log.Info( "Requesting permission for BluetoothAdmin" );
                              ActivityCompat.RequestPermissions(
                                 this,
                                 new[] {Manifest.Permission.BluetoothAdmin},
                                 24113 );
                           }
                        }
                        else
                        {
                           Log.Info( "Already have permission for BluetoothAdmin" );
                        }
                     } );
               } ),
            12000 );

         LoadApplication( new FormsApp( BluetoothLowEnergyAdapter.ObtainDefaultAdapter( ApplicationContext ) ) );
      }
   }

   internal sealed class TimerAction : TimerTask
   {
      private readonly Action m_action;

      public TimerAction( Action action )
      {
         m_action = action;
      }

      public override void Run()
      {
         m_action();
      }
   }
}