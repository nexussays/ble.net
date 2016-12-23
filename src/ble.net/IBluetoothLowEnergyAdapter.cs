// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace nexus.protocols.ble
{
   /// <summary>
   /// This is the entry point for all Bluetooth Low Energy functionality, use the platform-specific implementation of this
   /// interface to instantiate it. Use a <see cref="IBluetoothLowEnergyAdapter" /> to scan for BLE advertisements and connect
   /// to found devices.
   /// </summary>
   public interface IBluetoothLowEnergyAdapter : IDisposable
   {
      /// <summary>
      /// True if the adapter is currently enabled and operational
      /// </summary>
      /// TODO: move to some IAdapterState
      Boolean IsEnabled { get; }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />
      /// </summary>
      Task<IBleGattServer> ConnectToDevice( IBlePeripheral device, CancellationToken ct );

      /// <summary>
      /// Attempt to find and connect to the given device by ID, see <see cref="IBlePeripheral.DeviceId" /> for more information
      /// and relation to the device's MAC address
      /// </summary>
      Task<IBleGattServer> ConnectToDevice( Guid id, CancellationToken ct );

      /// <summary>
      /// The state of the bluetooth adapter and controls to toggle it on or off
      /// </summary>
      /// TODO: move to some IAdapterStateControl
      Task<Boolean> DisableAdapter();

      /// TODO: move to some IAdapterStateControl
      Task<Boolean> EnableAdapter();

      /// <summary>
      /// Scan for nearby BLE device advertisements. The devices discovered are not guaranteed to be unique, i.e. -- each device
      /// will likely be provided to the observer multiple times as the BLE scanner picks up advertisements.
      /// </summary>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      /// TODO: Add a method to scan with a filter on service, name, etc. Task ScanForDevices( ScanFilter filter, IObserver
      /// <IBluetoothLowEnergyPeripheral> advertisementDiscovered, CancellationToken ct );
      Task ScanForDevices( IObserver<IBlePeripheral> advertisementDiscovered, CancellationToken ct );
   }
}