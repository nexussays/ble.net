// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using Acr.UserDialogs;
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
         LoadApplication( new FormsApp( BluetoothLowEnergyAdapter.ObtainDefaultAdapter(), UserDialogs.Instance ) );
         return base.FinishedLaunching( app, options );
      }
   }
}