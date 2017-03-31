// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of an adapter
   /// </summary>
   public interface IAdapterState : IObservable<EnabledDisabledState>
   {
      /// <summary>
      /// True if the adapter is currently enabled and operational
      /// </summary>
      EnabledDisabledState CurrentState { get; }

      /// <summary>
      /// Register an observer to be notified when <see cref="CurrentState" /> has changed. This applies to external adapter
      /// changes as well as calls made to <see cref="IAdapterControl.EnableAdapter" /> or
      /// <see cref="IAdapterControl.DisableAdapter" />.
      /// </summary>
      new IDisposable Subscribe( IObserver<EnabledDisabledState> observer );
   }
}