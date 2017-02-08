// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble
{
   /// <summary>
   /// The state of the BLE device adapter.
   /// </summary>
   public enum BleAdapterState
   {
      /// <summary>
      /// The state of this adapter is unknown
      /// </summary>
      Unknown,
      /// <summary>
      /// The adapter is disabled and unavailable for use
      /// </summary>
      Disabled,
      /// <summary>
      /// The adapter is currently transitioning to <see cref="Disabled" />
      /// </summary>
      Disabling,
      /// <summary>
      /// The adapter is currently transitioning to <see cref="Enabled" />
      /// </summary>
      Enabling,
      /// <summary>
      /// The adapter is anabled and ready for use
      /// </summary>
      Enabled
   }
}