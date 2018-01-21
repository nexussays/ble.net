// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of an adapter and controls to enable or disable it.
   /// </summary>
   public interface IAdapterControl
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
      /// The current state of the adapter: either enabled, disabled, or transitioning to one or the other.
      /// </summary>
      IAdapterState CurrentState { get; }

      /// <summary>
      /// Disable this adapter system-wide.
      /// <remarks>Take care when using this as it affects other processes and apps running on the host device.</remarks>
      /// </summary>
      /// <returns><c>true</c> if the operation succeeded in disabling the adapter</returns>
      Task<Boolean> DisableAdapter();

      /// <summary>
      /// Enable this adapter system-wide.
      /// </summary>
      /// <returns><c>true</c> if the operation succeeded in enabling the adapter</returns>
      Task<Boolean> EnableAdapter();
   }
}