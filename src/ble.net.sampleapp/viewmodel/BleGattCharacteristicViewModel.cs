// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
using nexus.core.text;
using nexus.protocols.ble;
using nexus.protocols.ble.gatt;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   public class BleGattCharacteristicViewModel : BaseViewModel
   {
      private readonly IUserDialogs m_dialogManager;
      private readonly IBleGattServerConnection m_gattServer;
      private readonly Guid m_serviceGuid;
      private Guid m_characteristicGuid;
      private String m_descriptorValues;
      private Boolean m_isBusy;
      private IDisposable m_notificationSubscription;
      private CharacteristicProperty m_props;
      private String m_valueAsHex;
      private String m_valueAsString;
      private String m_writeValue;

      public BleGattCharacteristicViewModel( Guid serviceGuid, Guid characteristicGuid,
                                             IBleGattServerConnection gattServer, IUserDialogs dialogManager )
      {
         m_gattServer = gattServer;
         m_dialogManager = dialogManager;
         m_serviceGuid = serviceGuid;
         m_characteristicGuid = characteristicGuid;

         RefreshValueCommand = new Command( async () => { await ReadCharacteristicValue(); } );
         EnableNotificationsCommand = new Command( EnableNotifications );
         DisableNotificationsCommand = new Command( DisableNotifications );
         ToggleNotificationsCommand = new Command( () => { NotifyEnabled = !NotifyEnabled; } );
         WriteBytesCommand = new Command( async () => { await WriteCurrentBytes(); } );

         ValueAsHex = String.Empty;
         ValueAsString = String.Empty;

         m_gattServer.ReadCharacteristicProperties( m_serviceGuid, m_characteristicGuid ).ContinueWith(
            x =>
            {
               Device.BeginInvokeOnMainThread(
                  () =>
                  {
                     if(x.IsFaulted)
                     {
                        m_dialogManager.Toast( x.Exception.GetBaseException().Message );
                     }
                     else
                     {
                        Log.Trace( "Reading properties for characteristic. id={0}", m_characteristicGuid );
                        m_props = x.Result;
                        RaisePropertyChanged( nameof(CanNotify) );
                        RaisePropertyChanged( nameof(CanRead) );
                        RaisePropertyChanged( nameof(CanWrite) );
                     }
                  } );
            } );
      }

      public Boolean CanNotify => m_props.CanNotify() && !IsBusy;

      public Boolean CanRead => m_props.CanRead() && !IsBusy;

      public Boolean CanWrite => m_props.CanWrite() && !IsBusy;

      public String DescriptorValues
      {
         get { return m_descriptorValues; }
         private set { Set( ref m_descriptorValues, value ); }
      }

      public ICommand DisableNotificationsCommand { get; }

      public ICommand EnableNotificationsCommand { get; }

      public String Id => m_characteristicGuid.ToString();

      public Boolean IsBusy
      {
         get { return m_isBusy; }
         protected set
         {
            if(value != m_isBusy)
            {
               m_isBusy = value;
               RaiseCurrentPropertyChanged();
               RaisePropertyChanged( nameof(CanRead) );
               RaisePropertyChanged( nameof(CanNotify) );
               RaisePropertyChanged( nameof(CanWrite) );
            }
         }
      }

      public String Name => RegisteredAttributes.GetName( m_characteristicGuid ) ?? "Unknown Characteristic";

      public Boolean NotifyEnabled
      {
         get { return m_notificationSubscription != null; }
         set
         {
            if(value != (m_notificationSubscription != null))
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
         //m_notificationSubscription?.Dispose();
         //m_notificationSubscription = null;
      }

      public async Task UpdateDescriptors()
      {
         try
         {
            var descriptors = (await m_gattServer.ListCharacteristicDescriptors( m_serviceGuid, m_characteristicGuid ))
               .ToList();
            var vals = "";
            foreach(var desc in descriptors)
            {
               vals += desc + ": " +
                       await m_gattServer.ReadDescriptorValue( m_serviceGuid, m_characteristicGuid, desc ) + "\n";
            }
            DescriptorValues = vals;
         }
         catch(GattException ex)
         {
            Log.Warn( ex );
            m_dialogManager.Toast( ex.Message, TimeSpan.FromSeconds( 3 ) );
         }
      }

      private void DisableNotifications()
      {
         Log.Trace( "Disabling notifications for characteristic. id={0}", m_characteristicGuid );
         m_notificationSubscription?.Dispose();
         m_notificationSubscription = null;
         RaisePropertyChanged( nameof(NotifyEnabled) );
      }

      private void EnableNotifications()
      {
         if(m_notificationSubscription == null)
         {
            try
            {
               m_notificationSubscription = m_gattServer.NotifyCharacteristicValue(
                  m_serviceGuid,
                  m_characteristicGuid,
                  UpdateDisplayedValue );
            }
            catch(GattException ex)
            {
               m_dialogManager.Toast( ex.Message );
            }
         }
         RaisePropertyChanged( nameof(NotifyEnabled) );
      }

      private async Task ReadCharacteristicValue()
      {
         IsBusy = true;
         try
         {
            UpdateDisplayedValue( await m_gattServer.ReadCharacteristicValue( m_serviceGuid, m_characteristicGuid ) );
         }
         catch(GattException ex)
         {
            m_dialogManager.Toast( ex.Message );
         }
         IsBusy = false;
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
               var writeTask = m_gattServer.WriteCharacteristicValue( m_serviceGuid, m_characteristicGuid, val );
               // notify UI to clear written value from input field
               WriteValue = "";
               // update the characteristic value with the awaited results of the write
               UpdateDisplayedValue( await writeTask );
            }
            catch(GattException ex)
            {
               Log.Warn( ex.ToString() );
               m_dialogManager.Toast( ex.Message );
            }
            finally
            {
               IsBusy = false;
            }
         }
      }
   }
}
