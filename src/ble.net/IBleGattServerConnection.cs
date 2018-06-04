// Copyright M. Griffie <nexus@nexussays.com>
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using nexus.core;
using nexus.core.resharper;
using nexus.protocols.ble.gatt;
using nexus.protocols.ble.gatt.adopted;
using nexus.protocols.ble.scan;

namespace nexus.protocols.ble
{
   /// <summary>
   /// An active connection to a <see cref="IBlePeripheral" />. Obtain a <see cref="IBleGattServerConnection" /> by connecting
   /// to devices with
   /// <see cref="IBluetoothLowEnergyAdapter" />.
   /// <remarks>To observe the connection status and be aware of disconnections, see <see cref="Subscribe" />.</remarks>
   /// <remarks>
   /// The GATT Server connection is established when a Central device (e.g., the phone your app is running on) successfully
   /// establishes a client/server connection to a Peripheral device.
   /// </remarks>
   /// </summary>
   public interface IBleGattServerConnection
      : IBlePeripheral,
        IObservable<ConnectionState>,
        IEquatable<IBleGattServerConnection>,
        IDisposable
   {
      /// <summary>
      /// The current state of the connection to the device.
      /// Default: <see cref="ConnectionState.Disconnected" />
      /// </summary>
      ConnectionState State { get; }

      /// <summary>
      /// Disconnect from the GATT server and dispose of the connection.
      /// <remarks>
      /// To regain the connection, use <see cref="IBluetoothLowEnergyAdapter" />.
      /// </remarks>
      /// <remarks>To observe the connection status and be aware of (all) disconnections, see <see cref="Subscribe" />.</remarks>
      /// </summary>
      Task Disconnect();

      /// <summary>
      /// Disconnect from the device and dispose of this connection. Note that the device may disconnect in the background if
      /// necessary. To ensure  disconnection, await <see cref="Disconnect" /> instead of calling
      /// <see cref="Dispose" />.
      /// </summary>
      [ObsoleteEx(
         Message = "This functionality is now async",
         TreatAsErrorFromVersion = "2.0.0",
         RemoveInVersion = "2.1.0",
         ReplacementTypeOrMember = nameof(Disconnect) )]
      new void Dispose();

      /// <summary>
      /// Enumerate all services on this device
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request</exception>
      Task<IEnumerable<Guid>> ListAllServices();

      /// <summary>
      /// Enumerate all descriptors of the given characteristic. Descriptors are defined attributes that describe
      /// a characteristic value.
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request</exception>
      Task<IEnumerable<Guid>> ListCharacteristicDescriptors( Guid service, Guid characteristic );

      /// <summary>
      /// Enumerate all characteristics of the given service. Characteristics are defined attribute types
      /// that contain a single logical value.
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request</exception>
      Task<IEnumerable<Guid>> ListServiceCharacteristics( Guid service );

      /// <summary>
      /// Listen for NOTIFY or INDICATE events on this characteristic. Returns an <see cref="IDisposable" /> that removes the
      /// notify when <see cref="IDisposable.Dispose" /> is called.
      /// <remarks>
      /// The choice of INDICATE or NOTIFY is determined by the properties the server reports for this
      /// <paramref name="characteristic" /> (see: <see cref="ReadCharacteristicProperties" />)
      /// </remarks>
      /// </summary>
      IDisposable NotifyCharacteristicValue( Guid service, Guid characteristic,
                                             [NotNull] IObserver<Tuple<Guid, Byte[]>> observer );

      /// <summary>
      /// Read the properties of a characteristic
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request</exception>
      Task<CharacteristicProperty> ReadCharacteristicProperties( Guid service, Guid characteristic );

      /// <summary>
      /// Read the current value of a characteristic
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request</exception>
      Task<Byte[]> ReadCharacteristicValue( Guid service, Guid characteristic );

      /// <summary>
      /// Read the current value of this descriptor
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request</exception>
      Task<Byte[]> ReadDescriptorValue( Guid service, Guid characteristic, Guid descriptor );

      /// <summary>
      /// Check if the given service is present on the device.
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed.
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request.</exception>
      Task<Boolean> ServiceExists( Guid service );

      /// <summary>
      /// Be notified of changes to <see cref="State" />
      /// </summary>
      new IDisposable Subscribe( [NotNull] IObserver<ConnectionState> observer );

      /// <summary>
      /// Write the given <paramref name="data" /> to this characteristic's value, returning the bytes that were written (in
      /// almost all cases
      /// this returns a byte array identical to the <paramref name="data" /> passed as an argument).
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed.
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request.</exception>
      Task<Byte[]> WriteCharacteristicValue( Guid service, Guid characteristic, Byte[] data );

      /// <summary>
      /// Write the given <paramref name="data" /> to this descriptor, returning the bytes that were written (in almost all cases
      /// this returns a byte array identical to the <paramref name="data" /> passed as an argument).
      /// <remarks>For common descriptors, see static fields on <see cref="AdoptedDescriptors" /></remarks>
      /// </summary>
      /// <exception cref="GattServerConnectionLostException">
      /// If the connection has been lost before the request could be
      /// completed.
      /// </exception>
      /// <exception cref="GattException">If there was an error performing the request.</exception>
      Task<Byte[]> WriteDescriptorValue( Guid service, Guid characteristic, Guid descriptor, Byte[] data );
   }

   /// <summary>
   /// Extension methods for <see cref="IBleGattServerConnection" />
   /// </summary>
   [EditorBrowsable( EditorBrowsableState.Never )]
   public static class BleGattServerConnectionExtensions
   {
      /// <inheritdoc cref="IBleGattServerConnection.NotifyCharacteristicValue" />
      public static IDisposable NotifyCharacteristicValue( [NotNull] this IBleGattServerConnection server, Guid service,
                                                           Guid characteristic, [NotNull] Action<Guid, Byte[]> onNotify,
                                                           Action<Exception> onError = null )
      {
         if(server == null)
         {
            throw new ArgumentNullException( nameof(server) );
         }

         if(onNotify == null)
         {
            throw new ArgumentNullException( nameof(onNotify) );
         }

         return server.NotifyCharacteristicValue(
            service,
            characteristic,
            Observer.Create( ( Tuple<Guid, Byte[]> tuple ) => onNotify( tuple.Item1, tuple.Item2 ), null, onError ) );
      }

      /// <inheritdoc cref="IBleGattServerConnection.NotifyCharacteristicValue" />
      public static IDisposable NotifyCharacteristicValue( [NotNull] this IBleGattServerConnection server, Guid service,
                                                           Guid characteristic, [NotNull] Action<Byte[]> onNotify,
                                                           Action<Exception> onError = null )
      {
         if(server == null)
         {
            throw new ArgumentNullException( nameof(server) );
         }

         if(onNotify == null)
         {
            throw new ArgumentNullException( nameof(onNotify) );
         }

         return server.NotifyCharacteristicValue(
            service,
            characteristic,
            Observer.Create( ( Tuple<Guid, Byte[]> tuple ) => onNotify( tuple.Item2 ), null, onError ) );
      }
   }
}
