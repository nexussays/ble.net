// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
using nexus.protocols.ble;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public abstract class AbstractScanViewModel : BaseViewModel
   {
      protected readonly IBluetoothLowEnergyAdapter m_bleAdapter;
      protected readonly IUserDialogs m_dialogs;
      protected CancellationTokenSource m_scanCancel;
      private Boolean m_isScanning;

      protected AbstractScanViewModel( IBluetoothLowEnergyAdapter bleAdapter, IUserDialogs dialogs )
      {
         m_bleAdapter = bleAdapter;
         m_dialogs = dialogs;

         EnableAdapterCommand = new Command( async () => await ToggleAdapter( true ) );
         DisableAdapterCommand = new Command( async () => await ToggleAdapter( false ) );

         m_bleAdapter.CurrentState.Subscribe( state => { RaisePropertyChanged( nameof(IsAdapterEnabled) ); } );
      }

      public ICommand DisableAdapterCommand { get; }

      public ICommand EnableAdapterCommand { get; }

      public Boolean IsAdapterEnabled => m_bleAdapter.CurrentState.Value == EnabledDisabledState.Enabled ||
                                         m_bleAdapter.CurrentState.Value == EnabledDisabledState.Unknown;

      public Boolean IsScanning
      {
         get { return m_isScanning; }
         protected set { Set( ref m_isScanning, value ); }
      }

      public override void OnAppearing()
      {
         RaisePropertyChanged( nameof(IsAdapterEnabled) );
      }

      public override void OnDisappearing()
      {
         StopScan();
      }

      public void StopScan()
      {
         m_scanCancel?.Cancel();
      }

      private async Task ToggleAdapter( Boolean enable )
      {
         StopScan();
         try
         {
            await (enable ? m_bleAdapter.EnableAdapter() : m_bleAdapter.DisableAdapter());
         }
         catch(SecurityException ex)
         {
            m_dialogs.Toast( ex.Message );
            Log.Debug( ex, nameof(BleDeviceScannerViewModel) );
         }
         RaisePropertyChanged( nameof(IsAdapterEnabled) );
      }
   }
}
