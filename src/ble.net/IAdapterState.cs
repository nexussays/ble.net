// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of an adapter. Read <see cref="CurrentState" /> or <see cref="Subscribe" /> to listen for changes to the
   /// adapter state.
   /// </summary>
   public interface IAdapterState : IObservable<EnabledDisabledState>
   {
      /// <summary>
      /// The current state of the adapter either enabled, disabled, or transitioning to one of them.
      /// </summary>
      EnabledDisabledState CurrentState { get; }

      /// <summary>
      /// Register an observer to be notified when <see cref="CurrentState" /> has changed and to receive the new value. This is
      /// triggered both by external changes, and by calls made to <see cref="IAdapterControl.EnableAdapter" /> or
      /// <see cref="IAdapterControl.DisableAdapter" />.
      /// </summary>
      new IDisposable Subscribe( IObserver<EnabledDisabledState> observer );
   }
}