// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.core;

namespace nexus.protocols.ble
{
   internal sealed class Observable<T>
      : IObservable<T>,
        IDisposable
   {
      private readonly List<IObserver<T>> m_observers;

      public Observable()
      {
         m_observers = new List<IObserver<T>>();
      }

      public Boolean IsDisposed { get; private set; }

      public void Dispose()
      {
         ThrowIfDisposed();

         IsDisposed = true;
         foreach(var observer in m_observers)
         {
            observer.OnCompleted();
         }
         m_observers.Clear();
      }

      public void Error( Exception ex )
      {
         ThrowIfDisposed();

         foreach(var observer in m_observers)
         {
            observer.OnError( ex );
         }
      }

      public IDisposable Subscribe( IObserver<T> observer )
      {
         ThrowIfDisposed();

         m_observers.Add( observer );
         return new DisposeAction( () => m_observers.Remove( observer ) );
      }

      public void Update( T value )
      {
         ThrowIfDisposed();

         foreach(var observer in m_observers)
         {
            observer.OnNext( value );
         }
      }

      private void ThrowIfDisposed()
      {
         if(IsDisposed)
         {
            throw new InvalidOperationException( "Cannot perform operations on disposed {0}".F( GetType().Name ) );
         }
      }
   }
}