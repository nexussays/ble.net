// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble
{
   /// <summary>
   /// The result of a remote connection attempt
   /// </summary>
   public enum ConnectionResult
   {
      /// <summary>
      /// The connection attempt was unsuccessful
      /// </summary>
      UnknownFailure = 0,
      /// <summary>
      /// The local adapter is disabled and the connection could not be made
      /// </summary>
      AdapterDisabled,
      /// <summary>
      /// The remote device could not be found (prior to the connection attempt stopping) likely because the device is not in
      /// range or is not broadcasting.
      /// </summary>
      DeviceNotFound,
      /// <summary>
      /// The connection attempt was cancelled (e.g., cancellation token triggered) prior to the connection completing
      /// </summary>
      ConnectionAttemptCancelled,
      /// <summary>
      /// The remote device does not permit connections
      /// </summary>
      ConnectionNotAllowed,
      /// <summary>
      /// The connection attempt was successful
      /// </summary>
      Success = 0xff
   }
}