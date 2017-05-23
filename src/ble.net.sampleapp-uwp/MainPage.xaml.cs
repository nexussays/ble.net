using Acr.UserDialogs;
using nexus.protocols.ble;

namespace ble.net.sampleapp.uwp
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         LoadApplication( new FormsApp( BluetoothLowEnergyAdapter.ObtainDefaultAdapter(), UserDialogs.Instance ) );
      }
   }
}