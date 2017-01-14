using System;
using Foundation;
using nexus.protocols.ble;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ble.net.sampleapp.ios
{
   [Register( "AppDelegate" )]
   public class AppDelegate : FormsApplicationDelegate
   {
      public override Boolean FinishedLaunching( UIApplication app, NSDictionary options )
      {
         Forms.Init();
         LoadApplication( new FormsApp( BluetoothLowEnergyAdapter.ObtainDefaultAdapter() ) );

         return base.FinishedLaunching( app, options );
      }
   }
}