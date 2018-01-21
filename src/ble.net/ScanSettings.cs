using nexus.protocols.ble.scan;

namespace nexus.protocols.ble
{
   /// <summary>
   /// Options for
   /// <see
   ///    cref="IBluetoothLowEnergyAdapter.ScanForBroadcasts(nexus.protocols.ble.ScanSettings,System.IObserver{nexus.protocols.ble.scan.IBlePeripheral},System.Threading.CancellationToken)" />
   /// </summary>
   public struct ScanSettings
   {
      /// <inheritdoc cref="IScanFilter" />
      public IScanFilter Filter { get; set; }

      /// <inheritdoc cref="ScanMode" />
      public ScanMode Mode { get; set; }
   }
}