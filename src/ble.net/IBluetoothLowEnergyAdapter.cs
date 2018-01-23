// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using nexus.core;
using nexus.core.resharper;
using nexus.protocols.ble.scan;

namespace nexus.protocols.ble
{
   /// <summary>
   /// This is the entry point for all Bluetooth Low Energy functionality, use the platform-specific implementation of this
   /// interface to instantiate it. Use a <see cref="IBluetoothLowEnergyAdapter" /> to scan for BLE advertisements and connect
   /// to found devices.
   /// <remarks>
   /// There are also several extension methods available to make scanning and connecting easier. See
   /// <see cref="BluetoothLowEnergyAdapterExtensions" /> if you're reading this via API docs.
   /// </remarks>
   /// </summary>
   public interface IBluetoothLowEnergyAdapter : IAdapterControl
   {
      /// <summary>
      /// A list of all devices that have been discovered after
      /// <see
      ///    cref="ScanForBroadcasts(nexus.protocols.ble.scan.IScanFilter,System.IObserver{nexus.protocols.ble.scan.IBlePeripheral},System.Threading.CancellationToken)" />
      /// .
      /// <remarks>
      /// Devices can be purged from this list at indeterminate times depending on how recently an advertisement has
      /// been heard
      /// </remarks>
      /// </summary>
      IEnumerable<IBlePeripheral> DiscoveredPeripherals { get; }

      /// <summary>
      /// Attempt to connect to the provided <paramref name="device" />, and continue the attempt until <paramref name="ct" /> is
      /// cancelled.
      /// </summary>
      Task<BlePeripheralConnectionRequest> ConnectToDevice( [NotNull] IBlePeripheral device, CancellationToken ct,
                                                            IProgress<ConnectionProgress> progress = null );

      /// <summary>
      /// Attempt to find and connect to the device with given ID, and continue the attempt until <paramref name="ct" /> is
      /// cancelled. (see <see cref="IBlePeripheral.DeviceId" /> for more information on how <paramref name="id" /> relates to
      /// the device's MAC address)
      /// </summary>
      Task<BlePeripheralConnectionRequest> ConnectToDevice( Guid id, CancellationToken ct,
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
      /// <param name="scanSettings">
      /// Scan settings to configure scan mode and filter out certain advertisements, see:
      /// <see cref="ScanFilter" />
      /// </param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      Task ScanForBroadcasts( ScanSettings scanSettings, IObserver<IBlePeripheral> advertisementDiscovered,
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
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, Guid id, TimeSpan timeout,
         IProgress<ConnectionProgress> progress = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ConnectToDevice( id, new CancellationTokenSource( timeout ).Token, progress );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, Guid id, TimeSpan timeout,
         Action<ConnectionProgress> progress )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ConnectToDevice( adapter, id, timeout, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, Guid id, IProgress<ConnectionProgress> progress = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ConnectToDevice( adapter, id, BluetoothLowEnergyUtils.DefaultConnectionTimeout, progress );
      }

      /// <summary>
      /// Attempt to find and connect to the given device by MAC address (6 bytes). Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, Guid id, Action<ConnectionProgress> progress )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ConnectToDevice( adapter, id, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, IBlePeripheral device, TimeSpan timeout,
         IProgress<ConnectionProgress> progress = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ConnectToDevice( device, new CancellationTokenSource( timeout ).Token, progress );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout if the connection is not obtained in
      /// the provided time
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, IBlePeripheral device, TimeSpan timeout,
         Action<ConnectionProgress> progress )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ConnectToDevice( adapter, device, timeout, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, IBlePeripheral device,
         IProgress<ConnectionProgress> progress = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ConnectToDevice( adapter, device, BluetoothLowEnergyUtils.DefaultConnectionTimeout, progress );
      }

      /// <summary>
      /// Connect to a discovered <see cref="IBlePeripheral" />. Timeout after the default time;
      /// <see cref="BluetoothLowEnergyUtils.DefaultConnectionTimeout" />
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> ConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, IBlePeripheral device, Action<ConnectionProgress> progress )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ConnectToDevice( adapter, device, new Progress<ConnectionProgress>( progress ) );
      }

      /// <summary>
      /// Attempt to connect to the first device that passes the filter of <paramref name="settings" />, and continue the attempt
      /// until
      /// cancellation token is triggered.
      /// </summary>
      public static async Task<BlePeripheralConnectionRequest> FindAndConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, ScanSettings settings, CancellationToken ct,
         IProgress<ConnectionProgress> progress = null )
      {
         if(settings.Filter == null)
         {
            throw new ArgumentNullException(
               nameof(settings),
               "Scan settings must have a non-null ScanFilter when calling FindAndConnectToDevice" );
         }

         var cts = CancellationTokenSource.CreateLinkedTokenSource( ct );
         progress?.Report( ConnectionProgress.SearchingForDevice );
         var device = adapter.DiscoveredPeripherals.FirstOrDefault( p => settings.Filter.Passes( p.Advertisement ) );
         if(device == null)
         {
            await adapter.ScanForBroadcasts(
               settings,
               Observer.Create(
                  (Action<IBlePeripheral>)(p =>
                  {
                     device = p;
                     cts.Cancel();
                  }) ),
               cts.Token ).ConfigureAwait( false );
         }

         return device == null
            ? ct.IsCancellationRequested
               ? new BlePeripheralConnectionRequest( ConnectionResult.ConnectionAttemptCancelled, null )
               : new BlePeripheralConnectionRequest( ConnectionResult.DeviceNotFound, null )
            : await adapter.ConnectToDevice( device, ct, progress ).ConfigureAwait( false );
      }

      /// <summary>
      /// Attempt to connect to the first device that passes <paramref name="filter" />, and continue the attempt until
      /// <paramref name="timeout" /> has elapsed.
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> FindAndConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, ScanSettings settings, TimeSpan timeout,
         IProgress<ConnectionProgress> progress = null )
      {
         return FindAndConnectToDevice( adapter, settings, new CancellationTokenSource( timeout ).Token, progress );
      }

      /// <summary>
      /// Attempt to connect to the first device that passes <paramref name="filter" />, and continue the attempt until
      /// cancellation token is triggered.
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> FindAndConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, [NotNull] IScanFilter filter, CancellationToken ct,
         IProgress<ConnectionProgress> progress = null )
      {
         if(filter == null)
         {
            throw new ArgumentNullException( nameof(filter) );
         }

         return FindAndConnectToDevice( adapter, new ScanSettings {Filter = filter}, ct, progress );
      }

      /// <summary>
      /// Attempt to connect to the first device that passes <paramref name="filter" />, and continue the attempt until
      /// <paramref name="timeout" /> has elapsed.
      /// </summary>
      public static Task<BlePeripheralConnectionRequest> FindAndConnectToDevice(
         [NotNull] this IBluetoothLowEnergyAdapter adapter, [NotNull] IScanFilter filter, TimeSpan timeout,
         IProgress<ConnectionProgress> progress = null )
      {
         return FindAndConnectToDevice( adapter, new ScanSettings {Filter = filter}, timeout, progress );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning when <paramref name="ct" /> is cancelled.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan until this token is cancelled</param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter,
                                            Action<IBlePeripheral> advertisementDiscovered, CancellationToken ct )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ScanForBroadcasts( Observer.Create( advertisementDiscovered ), ct );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">
      /// cancel scan after this length of time. If <c>null</c>, stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter,
                                            IObserver<IBlePeripheral> advertisementDiscovered,
                                            TimeSpan? timeout = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ScanForBroadcasts(
            advertisementDiscovered,
            new CancellationTokenSource( timeout ?? BluetoothLowEnergyUtils.DefaultScanTimeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning after <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">
      /// cancel scan after this length of time. If <c>null</c>, stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter,
                                            Action<IBlePeripheral> advertisementDiscovered, TimeSpan? timeout = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ScanForBroadcasts( adapter, Observer.Create( advertisementDiscovered ), timeout );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning when <paramref name="ct" /> is cancelled.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="scanSettings">
      /// Scan settings to configure scan mode and filter out certain advertisements, see:
      /// <see cref="ScanFilter" />
      /// </param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="ct">Scan until this token is cancelled</param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter,
                                            ScanSettings scanSettings, Action<IBlePeripheral> advertisementDiscovered,
                                            CancellationToken ct )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ScanForBroadcasts( scanSettings, Observer.Create( advertisementDiscovered ), ct );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning when <paramref name="ct" /> is cancelled.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="scanSettings">
      /// Scan settings to configure scan mode and filter out certain advertisements, see:
      /// <see cref="ScanFilter" />
      /// </param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">
      /// cancel scan after this length of time. If <c>null</c>, stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter,
                                            ScanSettings scanSettings,
                                            IObserver<IBlePeripheral> advertisementDiscovered,
                                            TimeSpan? timeout = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ScanForBroadcasts(
            scanSettings,
            advertisementDiscovered,
            new CancellationTokenSource( timeout ?? BluetoothLowEnergyUtils.DefaultScanTimeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements. Stop scanning when <paramref name="ct" /> is cancelled.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="scanSettings">
      /// Scan settings to configure scan mode and filter out certain advertisements, see:
      /// <see cref="ScanFilter" />
      /// </param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">
      /// cancel scan after this length of time. If <c>null</c>, stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter,
                                            ScanSettings scanSettings, Action<IBlePeripheral> advertisementDiscovered,
                                            TimeSpan? timeout = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ScanForBroadcasts( scanSettings, Observer.Create( advertisementDiscovered ), timeout );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <see cref="ScanFilter" />
      /// </param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter, IScanFilter filter,
                                            IObserver<IBlePeripheral> advertisementDiscovered, CancellationToken ct )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return adapter.ScanForBroadcasts( new ScanSettings {Filter = filter}, advertisementDiscovered, ct );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />.
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <see cref="ScanFilter" />
      /// </param>
      /// <param name="ct">Scan will run continuously until this token is cancelled</param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter, IScanFilter filter,
                                            Action<IBlePeripheral> advertisementDiscovered, CancellationToken ct )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ScanForBroadcasts( adapter, filter, Observer.Create( advertisementDiscovered ), ct );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning after
      /// <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">
      /// cancel scan after this length of time. If <c>null</c>, stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match.
      /// <see cref="ScanFilter" />
      /// </param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter, IScanFilter filter,
                                            IObserver<IBlePeripheral> advertisementDiscovered,
                                            TimeSpan? timeout = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ScanForBroadcasts(
            adapter,
            filter,
            advertisementDiscovered,
            new CancellationTokenSource( timeout ?? BluetoothLowEnergyUtils.DefaultScanTimeout ).Token );
      }

      /// <summary>
      /// Scan for nearby BLE device advertisements that match <paramref name="filter" />. Stop scanning after
      /// <paramref name="timeout" />
      /// </summary>
      /// <param name="adapter">The adapter to use for scanning</param>
      /// <param name="advertisementDiscovered">Callback to notify for each discovered advertisement</param>
      /// <param name="timeout">
      /// cancel scan after this length of time. If <c>null</c>, stop scanning after
      /// <see cref="BluetoothLowEnergyUtils.DefaultScanTimeout" />
      /// </param>
      /// <param name="filter">
      /// Scan filter that will ignore broadcast advertisements that do not match. See <see cref="ScanFilter" />
      /// </param>
      public static Task ScanForBroadcasts( [NotNull] this IBluetoothLowEnergyAdapter adapter, IScanFilter filter,
                                            Action<IBlePeripheral> advertisementDiscovered, TimeSpan? timeout = null )
      {
         if(adapter == null)
         {
            throw new ArgumentNullException( nameof(adapter) );
         }

         return ScanForBroadcasts( adapter, filter, Observer.Create( advertisementDiscovered ), timeout );
      }
   }
}