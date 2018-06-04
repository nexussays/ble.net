// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ble.net.sampleapp.util
{
   public abstract class BaseViewModel : IBaseViewModel
   {
      /// <summary>
      /// Occurs after a property value changes.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      /// Provides access to the PropertyChanged event handler to derived classes.
      /// </summary>
      protected PropertyChangedEventHandler PropertyChangedHandler => PropertyChanged;

      public virtual void OnAppearing()
      {
      }

      public virtual void OnDisappearing()
      {
      }

      protected void RaiseCurrentPropertyChanged( [CallerMemberName] String propertyName = null )
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      protected void RaisePropertyChanged( String propertyName )
      {
         //VerifyPropertyName( propertyName );
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      /// <summary>
      /// If the new value is different than the current value, assign and raise PropertyChangedEvent
      /// </summary>
      protected Boolean Set<T>( ref T field, T newValue, [CallerMemberName] String propertyName = null )
      {
         if(EqualityComparer<T>.Default.Equals(field, newValue))
         {
            return false;
         }
         field = newValue;
         RaisePropertyChanged(propertyName);
         return true;
      }
   }
}
