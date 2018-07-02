<img src="http://public.nexussays.com/ble.net/logo_256x256.png" width="128" height="128" />

# [ble.net](https://www.fuget.org/packages/ble.net) [![Build status](https://img.shields.io/vso/build/nexussays/ebc6aafa-2931-41dc-b030-7f1eff5a28e5/7.svg?style=flat-square)](#) [![NuGet](https://img.shields.io/nuget/vpre/ble.net.svg?style=flat-square)](https://www.nuget.org/packages/ble.net) [![MPLv2 License](https://img.shields.io/badge/license-MPLv2-blue.svg?style=flat-square)](https://www.mozilla.org/MPL/2.0/)

`ble.net` is a Bluetooth Low Energy (aka BLE, aka Bluetooth LE, aka Bluetooth Smart) cross-platform library to enable simple development of BLE clients on Android, iOS, and UWP/Windows.

It provides a consistent API across all supported platforms and hides most of the problems and poor API decisions of the native BLE APIs. (Seriously, **every** OS has an absolutely horrible BLE API. I do not understand it.)

*[These projects are using BLE.net](https://github.com/nexussays/ble.net/wiki/Showcase)*

## Platform Support

|Platform|Version|
| ------------------- | :------------------: |
|Xamarin.iOS|iOS 7+|
|Xamarin.Android|API 18+|
|Windows 10 UWP|1511+|

> Note: Currently UWP only supports listening for broadcasts/advertisements, not connecting to devices.

## Quick example

This is a quick overview of the API and usage; continue reading below for setup instructions and more comprehensive examples.

```csharp
var connection = await ble.FindAndConnectToDevice(
      new ScanFilter().AddAdvertisedService( service ),
      TimeSpan.FromSeconds( 30 )
   );
if(connection.IsSuccessful())
{
   try
   {
      var gattServer = connection.GattServer;
      var read = gattServer.ReadCharacteristicValue( service, char1 );
      await Task.WhenAll( new Task[]
      {
         gattServer.WriteCharacteristicValue(
            service, char1, new Byte[]{/* bytes */}
         ),
         gattServer.WriteCharacteristicValue(
            service, char2, new Byte[]{/* bytes */}
         ),
         gattServer.WriteCharacteristicValue(
            service2, char3, new Byte[]{/* bytes */}
         )
      } );
      // Even though we await "read" after awaiting the write calls, the read was
      // dispatched first and so will have executed prior to the write calls
      originalValue = await read;
   }
   catch(GattException ex)
   {
      Debug.WriteLine( ex.ToString() );
   }
   finally
   {
     // The device will stay connected until you call Disconnect or the connection is lost through some external means.
     await connection.GattServer.Disconnect();
   }
}
```

## Getting Started

### 1. Install NuGet packages

#### In your .Net Standard or PCL or Shared project

> If you are targeting .Net Standard, add `<AssetTargetFallback>portable-net45+win8</AssetTargetFallback>` to your `.csproj`

Install the `ble.net (API)` package.
```
dotnet add package ble.net
```

#### In your Android or iOS or Windows (UWP) project

Install the relevant platform package.
```
dotnet add package ble.net-android
```
```
dotnet add package ble.net-ios
```
```
dotnet add package ble.net-uwp
```

### 2. Add relevant app permissions

#### Android

```csharp
[assembly: UsesPermission( Manifest.Permission.Bluetooth )]
[assembly: UsesPermission( Manifest.Permission.BluetoothAdmin )]
```

> If you are having issues discovering devices when scanning, try adding coarse location permissions. Android has inconsistent behavior across devices and adding this permission sometimes helps.
> ```csharp
> [assembly: UsesPermission( Manifest.Permission.AccessCoarseLocation )]
> ```
> Note also that this is a ["dangerous" permission](https://developer.android.com/guide/topics/permissions/requesting.html#normal-dangerous) in API 23+, so if you are targeting Android 6.0 or higher you will need to request this permission from the user at runtime.

#### iOS

If you are only using BLE in the foreground, when your app is active, you don't need to do any further setup for iOS.

If you need to use BLE in the background:
1. Add [bluetooth-central](https://developer.apple.com/library/content/documentation/NetworkingInternetWeb/Conceptual/CoreBluetooth_concepts/CoreBluetoothBackgroundProcessingForIOSApps/PerformingTasksWhileYourAppIsInTheBackground.html#//apple_ref/doc/uid/TP40013257-CH7-SW6) to background modes
2. Add a string value for key [NSBluetoothPeripheralUsageDescription](https://developer.apple.com/library/content/documentation/General/Reference/InfoPlistKeyReference/Articles/CocoaKeys.html#//apple_ref/doc/uid/TP40009251-SW20). iOS will display this value in a permission dialog box that the user must approve.

```xml
<!-- Info.plist -->
<key>UIBackgroundModes</key>
<array>
   <string>bluetooth-central</string>
</array>
<key>NSBluetoothPeripheralUsageDescription</key>
<string>[MyAppNameHere] would like to use bluetooth.</string>
```

#### UWP

```xml
<!-- Package.appxmanifest -->
<Capabilities>
   <DeviceCapability Name="bluetooth" />
</Capabilities>
```

### 3. Initialize `BluetoothLowEnergyAdapter` (Android-only)

> There is no initialization needed for iOS or UWP.

If you want `IBluetoothLowEnergyAdapter.DisableAdapter()` and `IBluetoothLowEnergyAdapter.EnableAdapter()` to work, then in your main `Activity` add:
```csharp
protected override void OnCreate( Bundle bundle )
{
   // ...
   BluetoothLowEnergyAdapter.InitActivity( this );
   // ...
}
```

If you want `IBluetoothLowEnergyAdapter.CurrentState.Subscribe()` to work, then in your calling `Activity` add:
```csharp
protected sealed override void OnActivityResult( Int32 requestCode, Result resultCode, Intent data )
{
   BluetoothLowEnergyAdapter.OnActivityResult( requestCode, resultCode, data );
}
```

### 4. Obtain a reference to `BluetoothLowEnergyAdapter`

Each platform project has a static method `BluetoothLowEnergyAdapter.ObtainDefaultAdapter()` which you call from your platform project and provide to your application code using whatever strategy you prefer (dependency injection, manual reference passing, a singleton or service locator, etc).

See the sample Xamarin Forms app for real-life examples:
* [Android Xamarin.Forms](src/ble.net.sampleapp-android/MyApplication.cs#L108) example
* [iOS Xamarin.Forms](src/ble.net.sampleapp-ios/MyApplication.cs#L59) example
* [UWP Xamarin.Forms](src/ble.net.sampleapp-uwp/MainPage.xaml.cs#L12) example

## API

> See [sample Xamarin Forms app](/src/ble.net.sampleapp/) included in the repo for further examples of how to integrate BLE.net into an app.

All the examples presume you have obtained the `IBluetoothLowEnergyAdapter` as per the setup notes above, e.g.:
```csharp
IBluetoothLowEnergyAdapter ble = /* platform-provided adapter from BluetoothLowEnergyAdapter.ObtainDefaultAdapter()*/;
```

### Control the Bluetooth Adapter on the device

#### Enable Bluetooth

> There are corresponding methods to disable the adapter.

```csharp
if(ble.AdapterCanBeEnabled && ble.CurrentState.IsDisabledOrDisabling()) {
   await ble.EnableAdapter();
}
```

#### See & Observe Adapter Status

```csharp
ble.CurrentState.Value; // e.g.: EnabledDisabledState.Enabled
// The adapter implements IObservable<EnabledDisabledState> so you can subscribe to its state
ble.CurrentState.Subscribe( state => Debug.WriteLine("New State: {0}", state) );
```

### Scan for advertisements being broadcast by nearby BLE peripherals

```csharp
await ble.ScanForBroadcasts(
   // Optional scan filter to ensure that the
   // observer will only receive peripherals
   // that pass the filter. If you want to scan
   // for everything around, omit this argument.
   new ScanFilter()
      .SetAdvertisedDeviceName( "foobar" )
      .SetAdvertisedManufacturerCompanyId( 76 )
      // Discovered peripherals must advertise at-least-one
      // of any GUIDs added by AddAdvertisedService()
      .AddAdvertisedService( guid )
      .SetIgnoreRepeatBroadcasts( false ),
   // IObserver<IBlePeripheral> or Action<IBlePeripheral>
   // will be triggered for each discovered peripheral
   // that passes the above can filter (if provided).
   ( IBlePeripheral peripheral ) =>
   {
      // read the advertising data...
      var adv = peripheral.Advertisement;
      Debug.WriteLine( adv.DeviceName );
      Debug.WriteLine( adv.Services.Select( x => x.ToString() ).Join( "," ) );
      Debug.WriteLine( adv.ManufacturerSpecificData.FirstOrDefault().CompanyName() );
      Debug.WriteLine( adv.ServiceData );

      // ...or connect to the device (see next example)...
   },
   // TimeSpan or CancellationToken to stop the scan
   TimeSpan.FromSeconds( 30 )
   // If you omit this argument, it will use
   // BluetoothLowEnergyUtils.DefaultScanTimeout
);

// scanning has been stopped when code reached this point
```

For the common case of ignoring duplicate advertisements (i.e., repeated advertisements from the same device), there is a static `ScanFilter.UniqueBroadcastsOnly` you can use as the scan filter.

You can also create a `ScanFilter` using an object initializer if you prefer that syntax:
```csharp
new ScanFilter()
{
   AdvertisedDeviceName = "foobar",
   AdvertisedManufacturerCompanyId = 76,
   AdvertisedServiceIsInList = new List<Guid>(){ guid },
   IgnoreRepeatBroadcasts = true
}
```

#### Setting antenna power when scanning

> Currently, this is only applicable to Android; it has no effect on other platforms.

```csharp
await ble.ScanForBroadcasts(
   new ScanSettings()
   {
      Mode = ScanMode.HighPower
      // or
      //Mode = ScanMode.LowPower,
      // if not provided, defaults to
      //Mode = ScanMode.Balanced
      Filter = // You can add your filter here as well
   },
   ( IBlePeripheral peripheral ) => { /* ... */ }
);
```

### Connect to a BLE device

If you have already scanned for and discovered a peripheral and you now want to connect to it:
```csharp
var connection = await ble.ConnectToDevice(
   // The IBlePeripheral to connect to
   peripheral,
   // TimeSpan or CancellationToken to stop the
   // connection attempt.
   // If you omit this argument, it will use
   // BluetoothLowEnergyUtils.DefaultConnectionTimeout
   TimeSpan.FromSeconds( 15 ),
   // Optional IProgress<ConnectionProgress>
   progress => Debug.WriteLine(progress)
);

if(connection.IsSuccessful())
{
   var gattServer = connection.GattServer;
   // ... do things with gattServer here... (see later examples...)
}
else
{
   // Do something to inform user or otherwise handle unsuccessful connection.
   Debug.WriteLine( "Error connecting to device. result={0:g}", connection.ConnectionResult );
   // e.g., "Error connecting to device. result=ConnectionAttemptCancelled"
}
```

Be sure to disconnect when you are done:

```csharp
await gattServer.Disconnect();
```

#### Connect to a specific device without manually scanning

In use-cases where you are not scanning for advertisements but rather looking to connect to a specific, known, device:
```csharp
var connection = await ble.FindAndConnectToDevice(
   new ScanFilter()
      .SetAdvertisedDeviceName( "foo" )
      .SetAdvertisedManufacturerCompanyId( 0xffff )
      .AddAdvertisedService( guid ),
   TimeSpan.FromSeconds( 30 ) );
if(connection.IsSuccessful())
{
   // ...
}
```

### See & Observe server connection Status

```csharp
Debug.WriteLine(gattServer.State); // e.g. ConnectionState.Connected
// the server implements IObservable<ConnectionState> so you can subscribe to its state
gattServer.Subscribe( state =>
{
   if(state == ConnectionState.Disconnected)
   {
      Debug.WriteLine("Connection Lost");
   }
} );
```

### Get and store descriptions for GATT GUIDs

You can provide information for the GUIDs representing services, characteristics, and descriptors with `KnownAttributes`.

```csharp
var known = new KnownAttributes();

// You can add descriptions for any desired
// characteristics, services, and descriptors
known.AddService( myGuid1, "Foo" );
known.AddCharacteristic( myGuid2, "Bar" );
known.AddDescriptor( myGuid3, "Baz" );
```

There are shortcuts to add all the attributes that have been adopted by the Bluetooth SIG.

```csharp
known.AddAdoptedServices();
known.AddAdoptedCharacteristics();
known.AddAdoptedDescriptors();
```

You can also create a new KnownAttributes with all the above adopted attributes already populated:

```csharp
known = KnownAttributes.CreateWithAdoptedAttributes();
```

Then use it as a lookup as needed.

```csharp
known.Get(guid);
known.GetDescriptionOrGuid(guid);
```

### Enumerate all services on the GATT Server

```csharp
foreach(var guid in await gattServer.ListAllServices())
{
   Debug.WriteLine( $"service: {known.GetDescriptionOrGuid(guid)}" );
}
```

### Enumerate all characteristics of a service

```csharp
Debug.WriteLine( $"service: {serviceGuid}" );
foreach(var guid in await gattServer.ListServiceCharacteristics( serviceGuid ))
{
   Debug.WriteLine( $"characteristic: {known.GetDescriptionOrGuid(guid)}" );
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
IDisposable notifyHandler;

try
{
   // Will also stop listening when gattServer
   // is disconnected, so if that is acceptable,
   // you don't need to store this disposable.
   notifyHandler = gattServer.NotifyCharacteristicValue(
      someServiceGuid,
      someCharacteristicGuid,
      // IObserver<Tuple<Guid, Byte[]>> or IObserver<Byte[]> or
      // Action<Tuple<Guid, Byte[]>> or Action<Byte[]>
      bytes => {/* do something with notification bytes */} );
}
catch(GattException ex)
{
   Debug.WriteLine( ex.ToString() );
}

// ... later, once done listening for notifications ...
notifyHandler.Dispose();
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
