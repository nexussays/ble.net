// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using nexus.protocols.ble.advertisement;

namespace nexus.protocols.ble
{
   internal abstract class BlePeripheralAdvertisement : IBlePeripheralAdvertisement
   {
      protected IDictionary<Int32, Byte[]> m_manufacturerData;
      protected IList<AdvertisingDataItem> m_rawData;
      protected IDictionary<Guid, Byte[]> m_serviceData;
      protected ISet<Guid> m_services;

      protected internal BlePeripheralAdvertisement()
      {
         m_services = new HashSet<Guid>();
         m_serviceData = new Dictionary<Guid, Byte[]>();
         m_manufacturerData = new Dictionary<Int32, Byte[]>();
         m_rawData = new List<AdvertisingDataItem>();
      }

      public String DeviceName { get; protected set; }

      public AdvertisingDataFlags Flags { get; protected set; }

      public IEnumerable<KeyValuePair<Int32, Byte[]>> ManufacturerSpecificData => m_manufacturerData;

      public IEnumerable<AdvertisingDataItem> RawData => m_rawData;

      public IEnumerable<KeyValuePair<Guid, Byte[]>> ServiceData => m_serviceData;

      public IEnumerable<Guid> Services => m_services;

      public Int32 TxPowerLevel { get; protected set; }
   }
}