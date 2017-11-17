<img src="http://public.nexussays.com/ble.net/logo_256x256.png" width="128" height="128" />

# ble.net ![Build status](https://img.shields.io/vso/build/nexussays/ebc6aafa-2931-41dc-b030-7f1eff5a28e5/7.svg?style=flat-square) [![NuGet](https://img.shields.io/nuget/v/ble.net.svg?style=flat-square)](https://www.nuget.org/packages/ble.net) [![MPLv2 License](https://img.shields.io/badge/license-MPLv2-blue.svg?style=flat-square)](https://www.mozilla.org/MPL/2.0/) [![API docs](https://img.shields.io/badge/apidocs-DotNetApis-blue.svg?style=flat-square)](http://dotnetapis.com/pkg/ble.net)

`ble.net` is a Bluetooth Low Energy (aka BLE, aka Bluetooth LE, aka Bluetooth Smart) cross-platform library to enable simple development of BLE clients on Android, iOS, and UWP/Windows.

It provides a consistent API across all supported platforms and hides many of the poor API decisions of each respective platform.

For example, you can make multiple simultaneous BLE requests on Android without worrying that some calls will silently fail. You can simply `await` all your calls without dealing with the book-keeping of an `event`-based system. If you know which characteristics and services you wish to interact with, then you can just read/write to them without having to query down into the device's attribute heirarchy and retain references to these characteristics and services. And so on...

> Note: Currently UWP only supports listening for broadcasts/advertisements, not connecting... the UWP BLE API is... proving difficult.

## Getting Started

### 1. Install NuGet packages

Install the `ble.net (API)` package in your (PCL/NetStandard) shared library
```powershell
Install-Package ble.net -Version 1.0.0-beta0007 
```

For each platform you are supporting, install the relevant package:

```powershell
Install-Package ble.net-android -Version 1.0.0-beta0007 
```
```powershell
Install-Package ble.net-ios -Version 1.0.0-beta0007 
```
```powershell
Install-Package ble.net-uwp -Version 1.0.0-beta0007 
```

### 2. Add relevant permissions

#### Android

```csharp
[assembly: UsesPermission( Manifest.Permission.Bluetooth )]
[assembly: UsesPermission( Manifest.Permission.BluetoothAdmin )]
```

If you are having issues discovering devices when scanning, try adding coarse location permissions.
```csharp
[assembly: UsesPermission( Manifest.Permission.AccessCoarseLocation )]
```

#### iOS

```xml
<!-- Info.plist -->
<key>NSBluetoothPeripheralUsageDescription</key>
<string>MyApp would like to use bluetooth.</string>
```

If you want to connect to peripherals in the background, add the [`bluetooth-central`](https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/CoreBluetooth_concepts/CoreBluetoothBackgroundProcessingForIOSApps/PerformingTasksWhileYourAppIsInTheBackground.html#//apple_ref/doc/uid/TP40013257-CH7-SW6) background mode.

```xml
<!-- Info.plist -->
<key>UIBackgroundModes</key>
<array>
   <string>bluetooth-central</string>
</array>
```

#### UWP

```xml
<!-- Package.appxmanifest -->
<Capabilities>
   <DeviceCapability Name="bluetooth" />
</Capabilities>
```

### 3. Obtain a reference to `BluetoothLowEnergyAdapter`

Each platform project has a static method `BluetoothLowEnergyAdapter.ObtainDefaultAdapter()` with various overloads. Obtain this reference and then provide it to your application code using your dependency injector, or manual reference passing, or a singleton, or whatever strategy you are using in your project.

Examples:
* [Android Xamarin.Forms](src/ble.net.sampleapp-android/MyApplication.cs#L98)
* [iOS Xamarin.Forms](src/ble.net.sampleapp-ios/MyApplication.cs#L64)
* [UWP Xamarin.Forms](src/ble.net.sampleapp-uwp/MainPage.xaml.cs#L12)

#### Android-specific setup

If you want `IBluetoothLowEnergyAdapter.State.DisableAdapter()` and `EnableAdapter()` to work, in your main `Activity`:
```csharp
protected override void OnCreate( Bundle bundle )
{
   // ...
   BluetoothLowEnergyAdapter.InitActivity( this );
   // ...
}
```

If you want `IBluetoothLowEnergyAdapter.State.Subscribe()` to work, in your calling `Activity`:
```csharp
protected sealed override void OnActivityResult( Int32 requestCode, Result resultCode, Intent data )
{
   BluetoothLowEnergyAdapter.OnActivityResult( requestCode, resultCode, data );
}
```

## API

> See [sample Xamarin Forms app](/src/ble.net.sampleapp/) included in the repo for a complete app example.

All the examples presume you have some `adapter` passed in as per the setup notes above:
```csharp
IBluetoothLowEnergyAdapter adapter = /* platform-provided adapter from BluetoothLowEnergyAdapter.ObtainDefaultAdapter()*/;
```

### Scan for broadcast advertisements

```csharp
await adapter.ScanForBroadcasts(
      ( IBlePeripheral peripheral ) =>
      {
         // read the advertising data...
         var ad = peripheral.Advertisement;
         Debug.WriteLine( ad.DeviceName );
         Debug.WriteLine( ad.Services.Select( x => x.ToString() ).Join( "," ) );
         Debug.WriteLine( ad.ManufacturerSpecificData.FirstOrDefault().CompanyName() );
         Debug.WriteLine( ad.ServiceData );

         // ...or connect to the device (see next example)...
      },
      // scan for 30 seconds and then stop
      // you can also provide a CancellationToken
      TimeSpan.FromSeconds( 30 ) );

// scanning has been stopped when code reached this point
```

You can also use a scan filter which will ensure that your callback only receives peripherals that pass the filter.

For the common case of ignoring duplicate advertisements (i.e., repeated advertisements from the same device), there is a static `ScanFilter.UniqueBroadcastsOnly` you can use as the scan filter.

Or write your own custom filter:
```csharp
// create the filter using an object initalizer...
await adapter.ScanForBroadcasts(
      new ScanFilter()
      {
         AdvertisedDeviceName = "foo",
         AdvertisedManufacturerCompanyId = 76,
         AdvertisedServiceIsInList = new List<Guid>(){ guid },
         IgnoreRepeatBroadcasts = true
      },
      p => { /* do stuff with found peripheral */ } );
// ...or create the filter using a fluent builder pattern
await adapter.ScanForBroadcasts(
      new ScanFilter()
         .SetAdvertisedDeviceName( "foo" )
         .SetAdvertisedManufacturerCompanyId( 76 )
         .AddAdvertisedService( guid )
         .SetIgnoreRepeatBroadcasts( true ),
      p => { /* do stuff with found peripheral */ } );
```

### Connect to a BLE device

If you just want to connect to a specific device, you can do so without scanning:
```csharp
var connection = await adapter.FindAndConnectToDevice(
   new ScanFilter()
      .SetAdvertisedDeviceName( "foo" )
      .SetAdvertisedManufacturerCompanyId( 0xffff )
      .AddAdvertisedService( guid ),
   TimeSpan.FromSeconds( 30 ) );
```

And if you have already scanned and discovered the peripheral you want to connect to:
```csharp
// If the connection isn't established before CancellationToken or timeout is triggered, it will be stopped.
var connection = await adapter.ConnectToDevice(
   peripheral,
   TimeSpan.FromSeconds( 15 ),
   progress => Debug.WriteLine(progress) );
```

Once you have `await`ed the connection result:
```csharp
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

### Get descriptions for GATT GUIDs

The names of all services, characteristics, and descriptors that have been adopted by the Bluetooth SIG can be stored in a `KnownAttributes` instance.

```csharp
var known = KnownAttributes.CreateWithAdoptedAttributes();
```

You can also add descriptions for custom attributes for GATT attributes you are using that aren't officially adopted:
```csharp
known.AddService( guid, "this is a service foo" );
//known.AddCharacteristic(...)
//known.AddDescriptor(...)
```

### Enumerate all services on the GATT Server

```csharp
foreach(var guid in await gattServer.ListAllServices())
{
   Debug.WriteLine( $"service: {guid} \"{known.Get(guid)?.Description}\"" );
}
```

### Enumerate all characteristics of a service

```csharp
Debug.WriteLine( $"service: {serviceGuid}" );
foreach(var guid in await gattServer.ListServiceCharacteristics( serviceGuid ))
{
   Debug.WriteLine( $"characteristic: {guid} \"{known.Get(guid)?.Description}\"" );
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
      // provide IObserver<Tuple<Guid, Byte[]>> or IObserver<Byte[]>
      // There are several extension methods to assist in creating the obvserver...
      bytes => {/* do something with notification bytes */} );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}
```

For stopping a notification listener prior to disconnecting from the device, `NotifyCharacteristicValue` returns an `IDisposable` that removes your notification observer when called:
```csharp
IDisposable notifier;

try
{
   notifier = gattServer.NotifyCharacteristicValue(
      someServiceGuid,
      someCharacteristicGuid,
      // provide IObserver<Tuple<Guid, Byte[]>> or IObserver<Byte[]>
      // There are several extension methods to assist in creating the obvserver...
      bytes => {/* do something with notification bytes */} );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}

// ... later, once done listening for notifications ...
notifier.Dispose();
```

### Write to a characteristic

```csharp
try
{
   // The resulting value of the characteristic is returned. In nearly all cases this
   // will be the same value that was provided to the write call (e.g. `byte[]{ 1, 2, 3 }`)
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

> (If you've used the native BLE APIs on Android or iOS, imagine the code you would have to write to achieve the same functionality as the following example.)

```csharp
try
{
   // dispatch a read characteristic request but don't await it yet
   var read = gattServer.ReadCharacteristicValue( /* arguments... */ );
   // perform multiple write operations and await them all
   await
      Task.WhenAll(
         new Task[]
         {
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            gattServer.WriteCharacteristicValue( /* arguments... */ ),
            gattServer.WriteCharacteristicValue( /* arguments... */ )
         } );
   // now await the read to ensure completion and return the result
   return await read;
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}
```
