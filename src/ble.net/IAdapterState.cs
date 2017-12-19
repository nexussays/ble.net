// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of an adapter. Read <see cref="Value" /> or <see cref="Subscribe" /> to listen for changes to the
   /// adapter state.
   /// </summary>
   public interface IAdapterState : IObservable<EnabledDisabledState>
   {
      /// <summary>
      /// The state of the adapter.
      /// </summary>
      EnabledDisabledState Value { get; }

      /// <summary>
      /// Register an observer to be notified when <see cref="Value" /> has changed and to receive the new value. This is
      /// triggered both by external changes, and by calls made to <see cref="IAdapterControl.EnableAdapter" /> or
      /// <see cref="IAdapterControl.DisableAdapter" />.
      /// </summary>
      new IDisposable Subscribe( IObserver<EnabledDisabledState> observer );
   }
}