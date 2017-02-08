// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading;
using System.Threading.Tasks;
using nexus.protocols.ble.connection;

namespace nexus.protocols.ble
{
   /// <summary>
   /// This is the entry point for all Bluetooth Low Energy functionality, use the platform-specific implementation of this
   /// interface to instantiate it. Use a <see cref="IBluetoothLowEnergyAdapter" /> to scan for BLE advertisements and connect
   /// to found devices.
   /// </summary>
   public interface IBluetoothLowEnergyAdapter
   {
      /// <summary>
      /// The state of this BLE adapter and controls to enable or disable it
      /// </summary>
      IAdapterStateControl State { get; }

      /// <summary>
      /// Attempt to connect to the provided <paramref name="device" />, and continue the attempt until <paramref name="ct" /> is
      /// cancelled.
      /// </summary>
      Task<BleDeviceConnection> ConnectToDevice( IBlePeripheral device, CancellationToken ct,
                                                 IProgress<ConnectionProgress> progress = null );

      /// <summary>
      /// Attempt to find and connect to the device with given ID, and continue the attempt until <paramref name="ct" /> is
      /// cancelled. (see <see cref="IBlePeripheral.DeviceId" /> for more information on how <paramref name="id" /> relates to
      /// the device's MAC address)
      /// </summary>
      Task<BleDeviceConnection> ConnectToDevice( Guid id, CancellationToken ct,
                                                 IProgress<ConnectionProgress> progress = null );

      /// <summary>
      /// Scan for nearby BLE device advertisements. The devices discovered are not guaranteed to be unique, i.e. -- each device
      /// will likely be provided to the observer multiple times as the BLE scanner picks up advertisements.
      /// </summary>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      Task ScanForBroadcasts( IObserver<IBlePeripheral> advertisementDiscovered, CancellationToken ct );

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. The devices discovered are not
      /// guaranteed to be unique, i.e. -- each device
      /// will likely be provided to the observer multiple times as the BLE scanner picks up advertisements.
      /// </summary>
      /// <param name="filter">Scan filter that will ignore broadcast advertisements that do not match. see: <see cref="ScanFilter.Factory"/></param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      Task ScanForBroadcasts( ScanFilter filter, IObserver<IBlePeripheral> advertisementDiscovered, CancellationToken ct );
   }
}