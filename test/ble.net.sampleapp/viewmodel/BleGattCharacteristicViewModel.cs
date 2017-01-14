// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
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
   public class BleGattCharacteristicViewModel : BaseViewModel
   {
      private readonly IBleGattServer m_device;
      private readonly IUserDialogs m_dialogs;
      private readonly Guid m_service;
      private Guid m_characteristic;
      private Boolean m_isBusy;
      private CharacteristicProperty m_props;
      private IDisposable m_unsubscribe;
      private String m_valueAsHex;
      private String m_valueAsString;
      private String m_writeValue;

      public BleGattCharacteristicViewModel( Guid service, Guid characteristic, IBleGattServer device,
                                             IUserDialogs dialogs )
      {
         m_device = device;
         m_dialogs = dialogs;
         m_service = service;
         m_characteristic = characteristic;

         RefreshValueCommand = new Command( async () => { await ReadValue(); } );
         EnableNotificationsCommand = new Command( EnableNotifications );
         DisableNotificationsCommand = new Command( DisableNotifications );
         ToggleNotificationsCommand = new Command( ToggleNotifications );
         WriteBytesCommand = new Command( async () => { await WriteCurrentBytes(); } );

         ValueAsHex = String.Empty;
         ValueAsString = String.Empty;

         m_device.ReadCharacteristicProperties( m_service, m_characteristic ).ContinueWith(
                    x =>
                    {
                       Device.BeginInvokeOnMainThread(
                          () =>
                          {
                             //Log.Trace( "Reading properties for characteristic. id={0}", m_characteristic );
                             m_props = x.Result;
                             RaisePropertyChanged( nameof( CanNotify ) );
                             RaisePropertyChanged( nameof( CanRead ) );
                             RaisePropertyChanged( nameof( CanWrite ) );
                          } );
                    } );
      }

      public Boolean CanNotify => m_props.CanNotify();

      public Boolean CanRead => m_props.CanRead();

      public Boolean CanWrite => m_props.CanWrite();

      public ICommand DisableNotificationsCommand { get; }

      public ICommand EnableNotificationsCommand { get; }

      public String Id => m_characteristic.ToString();

      public Boolean IsBusy
      {
         get { return m_isBusy; }
         protected set { Set( ref m_isBusy, value ); }
      }

      public String Name => null /*KnownAttributes.Get( m_characteristic )?.Description */?? Id;

      public Boolean NotifyEnabled
      {
         get { return m_unsubscribe != null; }
         set
         {
            if(value != (m_unsubscribe != null))
            {
               if(value)
               {
                  EnableNotifications();
               }
               else
               {
                  DisableNotifications();
               }
               RaiseCurrentPropertyChanged();
            }
         }
      }

      public ICommand RefreshValueCommand { get; }

      public ICommand ToggleNotificationsCommand { get; }

      public String ValueAsHex
      {
         get { return m_valueAsHex; }
         private set { Set( ref m_valueAsHex, value ); }
      }

      public String ValueAsString
      {
         get { return m_valueAsString; }
         private set { Set( ref m_valueAsString, value ); }
      }

      public ICommand WriteBytesCommand { get; }

      public String WriteValue
      {
         get { return m_writeValue; }
         set { Set( ref m_writeValue, value ); }
      }

      public override void OnDisappearing()
      {
         //m_unsubscribe?.Dispose();
         //m_unsubscribe = null;
      }

      public void Update( Guid ch, Byte[] value )
      {
         if(!m_characteristic.Equals( ch ))
         {
            Log.Warn( "Characteristics differ. ch1={0} ch2={1}", m_characteristic, ch );
            return;
         }
         UpdateDisplayedValue( value );
      }

      private void DisableNotifications()
      {
         Log.Trace( "DisableNotifications" );
         m_unsubscribe?.Dispose();
         m_unsubscribe = null;
         RaisePropertyChanged( nameof( NotifyEnabled ) );
      }

      private void EnableNotifications()
      {
         if(m_unsubscribe == null)
         {
            Log.Trace( "EnableNotifications" );
            try
            {
               m_unsubscribe = m_device.NotifyCharacteristicValue(
                  m_service,
                  m_characteristic,
                  Observer.Create<Tuple<Guid, Byte[]>>( res => Update( res.Item1, res.Item2 ) ) );
            }
            catch(GattException ex)
            {
               m_dialogs.Toast( ex.Message );
            }
         }
         RaisePropertyChanged( nameof( NotifyEnabled ) );
      }

      private async Task ReadValue()
      {
         IsBusy = true;
         try
         {
            UpdateDisplayedValue( await m_device.ReadCharacteristicValue( m_service, m_characteristic ) );
         }
         catch(GattException ex)
         {
            m_dialogs.Toast( ex.Message );
         }
         IsBusy = false;
      }

      private void ToggleNotifications()
      {
         if(NotifyEnabled)
         {
            DisableNotifications();
         }
         else
         {
            EnableNotifications();
         }
      }

      private void UpdateDisplayedValue( Byte[] bytes )
      {
         ValueAsHex = bytes.EncodeToBase16String();
         try
         {
            ValueAsString = bytes.AsUtf8String();
         }
         catch
         {
            ValueAsString = String.Empty;
         }
      }

      private async Task WriteCurrentBytes()
      {
         var w = m_writeValue;
         if(!w.IsNullOrEmpty())
         {
            var val = w.DecodeAsBase16();
            try
            {
               IsBusy = true;
               var task = m_device.WriteCharacteristicValue( m_service, m_characteristic, val );
               WriteValue = "";
               UpdateDisplayedValue( await task );
            }
            catch(GattException ex)
            {
               Log.Warn( ex.Message );
               m_dialogs.Toast( ex.Message );
            }
            finally
            {
               IsBusy = false;
            }
         }
         else
         {
            Log.Info( "Nothing to write" );
         }
      }
   }
}