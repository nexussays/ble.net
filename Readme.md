<img src="http://public.nexussays.com/ble.net/logo_256x256.png" width="128" height="128" />

# ble.net ![Build status](https://img.shields.io/vso/build/nexussays/ebc6aafa-2931-41dc-b030-7f1eff5a28e5/7.svg?style=flat-square) [![NuGet](https://img.shields.io/nuget/v/ble.net.svg?style=flat-square)](https://www.nuget.org/packages/ble.net) [![MPLv2 License](https://img.shields.io/badge/license-MPLv2-blue.svg?style=flat-square)](https://www.mozilla.org/MPL/2.0/) [![API docs](https://img.shields.io/badge/apidocs-DotNetApis-blue.svg?style=flat-square)](http://dotnetapis.com/pkg/ble.net)

`ble.net` is a Bluetooth Low Energy (aka BLE, aka Bluetooth LE, aka Bluetooth Smart) PCL to enable simple development of BLE clients on Android, iOS, and Windows 10.

> Currently Windows 10 / UWP only supports observing broadcasts/advertisements.

## Setup

### 1. Install NuGet packages

Install `ble.net` in your PCL
```powershell
Install-Package ble.net
```

In each platform project, install the relevant package:
```powershell
Install-Package ble.net-android
```
```powershell
Install-Package ble.net-ios
```
```powershell
Install-Package ble.net-uwp
```

> Note that there are two packages for Android, `ble.net-android` and `ble.net-android21`; the API 21+ package is not materially different for client operations but will be needed if/when server operations are implemented. You should probably just use `ble.net-android`.

### 2. Obtain a reference to `BluetoothLowEnergyAdapter`

Each platform project has a static method `BluetoothLowEnergyAdapter.ObtainDefaultAdapter()`. Obtain this reference and then provide it to your application code using whatever dependency injector or manual reference passing you are using in your project.

#### Android-specific setup

If you want the adapter enable/disable functions to work, in your main `Activity`:
```csharp
protected override void OnCreate( Bundle bundle )
{
   // ...

   BluetoothLowEnergyAdapter.InitActivity( this );

   // ...
}
```

If you want `IBluetoothLowEnergyAdapter.OnStateChanged` to work, in your calling `Activity`:
```csharp
protected sealed override void OnActivityResult( Int32 requestCode, Result resultCode, Intent data )
{
   BluetoothLowEnergyAdapter.OnActivityResult( requestCode, resultCode, data );
}
```

## API

> See [sample Xamarin Forms app](/test/ble.net.sampleapp/) included in the repo for a complete example.

All the examples presume you have some `adapter` passed in as per the setup notes above:
```csharp
IBluetoothLowEnergyAdapter adapter = /* platform provided adapter value */;
```

### Scan for broadcast advertisements

```csharp
await adapter.ScanForDevices(
      ( IBlePeripheral peripheral ) =>
      {
         // read the advertising data...
         //peripheral.Advertisement.DeviceName
         //peripheral.Advertisement.Services
         //peripheral.Advertisement.ManufacturerSpecificData
         //peripheral.Advertisement.ServiceData

         // ...or connect to the device (see next example)...
      },
      // scan for 30 seconds and then stop
      TimeSpan.FromSeconds( 30 ) );

// scanning has completed
```

### Connect to a BLE device

```csharp
// If the connection isn't established before timeout (or a CancellationToken) is triggered, it will be stopped
var connection = await adapter.ConnectToDevice( peripheral, TimeSpan.FromSeconds( 5 ));
if(connection.IsSuccessful())
{
   var gattServer = connection.GattServer;
   // do things with gattServer here... (see further examples...)
}
else
{
   // Do something to inform user or otherwise handle unsuccessful connection.
   Debug.WriteLine( "Error connecting to device. result={0:g}", connection.ConnectionResult );
   // e.g., "Error connecting to device. result=ConnectionAttemptCancelled"
}
```

Connection also optionally takes an `IProgress` argument and has several utility overloads, e.g.:
```csharp
var connection = await adapter.ConnectToDevice( peripheral, ct, progress => Debug.WriteLine(progress) );
```

### Enumerate all services on the GATT Server

```csharp
foreach(var guid in await gattServer.ListAllServices())
{
   Debug.WriteLine( $"service: {guid}" );
}
```

### Enumerate all characteristics of a service

```csharp
Debug.WriteLine( $"service: {serviceGuid}" );
foreach(var guid in await gattServer.ListServiceCharacteristics( serviceGuid ))
{
   Debug.WriteLine( $"characteristic: {guid}" );
}
```

### Read a characteristic

```csharp
try
{
   var value = await gattServer.ReadCharacteristicValue( someServiceGuid, someCharacteristicGuid );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}
```

### Listen for notifications on a characteristic

```csharp
try
{
   // will stop listening when gattServer is disconnected
   gattServer.NotifyCharacteristicValue(
      someServiceGuid,
      someCharacteristicGuid,
      // Observer takes Tuple<Guid, Byte[]> or Byte[]
      bytes => {/* do something with notification bytes */} );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}
```

For stopping a notification listener, `NotifyCharacteristicValue` returns an `IDisposable` that removes your notification observer when called:
```csharp
IDisposable notifier;

try
{
   notifier = gattServer.NotifyCharacteristicValue(
      someServiceGuid,
      someCharacteristicGuid,
      // Observer takes Tuple<Guid, Byte[]> or Byte[]
      bytes => {/* do something with notification bytes */} );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}

// ... later ...
// done listening for notifications
notifier.Dispose();
```

### Write to a characteristic

```csharp
try
{
   // the resulting value of the characteristic is returned
   var value = await gattServer.WriteCharacteristicValue(
      someServiceGuid,
      someCharacteristicGuid,
      new byte[]{ 1, 2, 3 } );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}
```

### Do a bunch of things

```csharp
try
{
   var read = gattServer.ReadCharacteristicValue( /* arguments... */ );
   await
      Task.WhenAll(
         new Task[]
         {
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            read
         } );

   return await read;
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}
```