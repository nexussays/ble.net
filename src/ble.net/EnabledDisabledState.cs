// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.protocols.ble
{
   /// <summary>
   /// Represents the state of some entity that can be enabled and disabled
   /// </summary>
   public enum EnabledDisabledState
   {
      /// <summary>
      /// The state of this entity is unknown
      /// </summary>
      Unknown,

      /// <summary>
      /// The entity is disabled and unavailable for use
      /// </summary>
      Disabled,
      /// <summary>
      /// The entity is currently transitioning to <see cref="Disabled" />
      /// </summary>
      Disabling,

      /// <summary>
      /// The entity is currently transitioning to <see cref="Enabled" />
      /// </summary>
      Enabling,
      /// <summary>
      /// The entity is enabled and ready for use
      /// </summary>
      Enabled
   }
}