// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using nexus.core;
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
      IAdapterControl State { get; }

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
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match. see:
      /// <see cref="ScanFilter.Factory" />
      /// </param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      Task ScanForBroadcasts( ScanFilter filter, IObserver<IBlePeripheral> advertisementDiscovered,
                              CancellationToken ct );
   }

   /// <summary>
   /// Extension methods for <see cref="IBluetoothLowEnergyAdapter" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class BluetoothLowEnergyAdapterExtensions
   {
      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, Guid id,
                                                               TimeSpan timeout,
                                                               IProgress<ConnectionProgress> progress = null )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         // ReSharper disable once PossibleNullReferenceException
         return adapter.ConnectToDevice( id, new CancellationTokenSource( timeout ).Token, progress );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, Guid id,
                                                               TimeSpan timeout, Action<ConnectionProgress> progress )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ConnectToDevice( adapter, id, timeout, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, Guid id,
                                                               IProgress<ConnectionProgress> progress = null )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ConnectToDevice( adapter, id, BluetoothLowEnergyUtils.DefaultConnectionTimeout, progress );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter, Guid id,
                                                               Action<ConnectionProgress> progress )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ConnectToDevice( adapter, id, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter,
                                                               IBlePeripheral device, TimeSpan timeout,
                                                               IProgress<ConnectionProgress> progress = null )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         // ReSharper disable once PossibleNullReferenceException
         return adapter.ConnectToDevice( device, new CancellationTokenSource( timeout ).Token, progress );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter,
                                                               IBlePeripheral device, TimeSpan timeout,
                                                               Action<ConnectionProgress> progress )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ConnectToDevice( adapter, device, timeout, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter,
                                                               IBlePeripheral device,
                                                               IProgress<ConnectionProgress> progress = null )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ConnectToDevice( adapter, device, BluetoothLowEnergyUtils.DefaultConnectionTimeout, progress );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BleDeviceConnection> ConnectToDevice( this IBluetoothLowEnergyAdapter adapter,
                                                               IBlePeripheral device,
                                                               Action<ConnectionProgress> progress )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ConnectToDevice( adapter, device, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">cancel scan after this length of time</param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter,
                                            IObserver<IBlePeripheral> advertisementDiscovered, TimeSpan timeout )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         // ReSharper disable once PossibleNullReferenceException
         return adapter.ScanForBroadcasts( advertisementDiscovered, new CancellationTokenSource( timeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning after
      /// <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">cancel scan after this length of time</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <see cref="ScanFilter.Factory" />
      /// </param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter, ScanFilter filter,
                                            IObserver<IBlePeripheral> advertisementDiscovered, TimeSpan timeout )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         // ReSharper disable once PossibleNullReferenceException
         return adapter.ScanForBroadcasts(
            filter,
            advertisementDiscovered,
            new CancellationTokenSource( timeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter,
                                            IObserver<IBlePeripheral> advertisementDiscovered )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ScanForBroadcasts( adapter, advertisementDiscovered, BluetoothLowEnergyUtils.DefaultScanTimeout );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <see cref="ScanFilter.Factory" />
      /// </param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter, ScanFilter filter,
                                            IObserver<IBlePeripheral> advertisementDiscovered )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ScanForBroadcasts(
            adapter,
            filter,
            advertisementDiscovered,
            BluetoothLowEnergyUtils.DefaultScanTimeout );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning when <paramref name="token" /> is cancelled.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="token">Scan until this token is cancelled</param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter,
                                            Action<IBlePeripheral> advertisementDiscovered, CancellationToken token )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         // ReSharper disable once PossibleNullReferenceException
         return adapter.ScanForBroadcasts( Observer.Create( advertisementDiscovered ), token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">cancel scan after this length of time</param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter,
                                            Action<IBlePeripheral> advertisementDiscovered, TimeSpan timeout )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ScanForBroadcasts( adapter, advertisementDiscovered, new CancellationTokenSource( timeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter,
                                            Action<IBlePeripheral> advertisementDiscovered )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ScanForBroadcasts( adapter, advertisementDiscovered, BluetoothLowEnergyUtils.DefaultScanTimeout );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning when
      /// <paramref name="ct" /> is cancelled.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <c>new ScanFilter.Factory(){}.CreateFilter()</c>
      /// </param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter, ScanFilter filter,
                                            Action<IBlePeripheral> advertisementDiscovered, CancellationToken ct )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         // ReSharper disable once PossibleNullReferenceException
         return adapter.ScanForBroadcasts( filter, Observer.Create( advertisementDiscovered ), ct );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning after
      /// <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">cancel scan after this length of time</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <c>new ScanFilter.Factory(){}.CreateFilter()</c>
      /// </param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter, ScanFilter filter,
                                            Action<IBlePeripheral> advertisementDiscovered, TimeSpan timeout )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ScanForBroadcasts(
            adapter,
            filter,
            advertisementDiscovered,
            new CancellationTokenSource( timeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <c>new ScanFilter.Factory(){}.CreateFilter()</c>
      /// </param>
      public static Task ScanForBroadcasts( this IBluetoothLowEnergyAdapter adapter, ScanFilter filter,
                                            Action<IBlePeripheral> advertisementDiscovered )
      {
         Contract.Requires<ArgumentNullException>( adapter != null );
         return ScanForBroadcasts(
            adapter,
            filter,
            advertisementDiscovered,
            BluetoothLowEnergyUtils.DefaultScanTimeout );
      }
   }
}