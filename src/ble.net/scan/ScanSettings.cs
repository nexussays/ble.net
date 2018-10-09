using System;

namespace nexus.protocols.ble.scan
{
   /// <summary>
   /// Options for
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ScanForBroadcasts(nexus.protocols.ble.scan.ScanSettings,System.IObserver{nexus.protocols.ble.scan.IBlePeripheral},System.Threading.CancellationToken)" />
   /// </summary>
   public struct ScanSettings
   {
      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      /// <remarks>Syntax sugar for <c>new ScanSettings {IgnoreRepeatBroadcasts = true}</c></remarks>
      public static readonly ScanSettings UniqueBroadcastsOnly = new ScanSettings {IgnoreRepeatBroadcasts = true};

      /// <inheritdoc cref="IScanFilter" />
      public IScanFilter Filter { get; set; }

      /// <summary>
      /// Each discovered device will be provided to your observer once, and any additional broadcasts detected during this scan
      /// will be ignored.
      /// </summary>
      public Boolean IgnoreRepeatBroadcasts { get; set; }

      /// <inheritdoc cref="ScanMode" />
      public ScanMode Mode { get; set; }
   }
}
