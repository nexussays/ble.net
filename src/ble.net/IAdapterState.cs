// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of an adapter and controls to enable or disable it
   /// </summary>
   public interface IAdapterStateControl : IAdapterState
   {
      /// <summary>
      /// <c>true</c> if the current platform allows disabling this adapter
      /// </summary>
      Boolean AdapterCanBeDisabled { get; }

      /// <summary>
      /// <c>true</c> if the current platform allows enabling this adapter
      /// </summary>
      Boolean AdapterCanBeEnabled { get; }

      /// <summary>
      /// Disable this adapter system-wide
      /// </summary>
      /// <returns></returns>
      Task<Boolean> DisableAdapter();

      /// <summary>
      /// Enable this adapter system-wide
      /// </summary>
      Task<Boolean> EnableAdapter();
   }

   public interface IAdapterState : IObservable<BleAdapterState>
   {
      /// <summary>
      /// True if the adapter is currently enabled and operational
      /// </summary>
      BleAdapterState CurrentState { get; }

      /// <summary>
      /// Register an observer to be notified when <see cref="CurrentState" /> has changed. This applies to external adapter
      /// changes as well as calls made to <see cref="IAdapterStateControl.EnableAdapter" /> or
      /// <see cref="IAdapterStateControl.DisableAdapter" />.
      /// </summary>
      new IDisposable Subscribe( IObserver<BleAdapterState> observer );
   }
}