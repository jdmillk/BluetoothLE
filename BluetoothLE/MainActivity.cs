using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth.LE;
using Android.Bluetooth;
using Android.Content;
using Plugin.BLE;
using Adapter = Plugin.BLE.Android.Adapter;

namespace BluetoothLE
{

    [Activity(Label = "BluetoothLE", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button btScan;
        ConditionVariable adaptador;
        private BluetoothLeScanner bleScanner;
        private BluetoothManager manager;
        private BluetoothAdapter adapter;
        private ListView res;
        private Adapter bleAdapter;
        private static int SCAN_PERIOD = 10000;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            btScan = FindViewById<Button>(Resource.Id.btScan);
            res = FindViewById<ListView>(Resource.Id.lvLista);

            var context = Application.Context;
            manager = (BluetoothManager)context.GetSystemService(BluetoothService);
            adapter = manager.Adapter;




            bleAdapter = new Adapter(adapter);
            bleAdapter.DeviceDiscovered += Adaptador_DeviceDiscovered;
            bleAdapter.ScanTimeoutElapsed += Adaptador_ScanTimeoutElapsed;





            btScan.Click += delegate
            {
                if (bleAdapter.IsScanning)
                    bleAdapter.StartScanningForDevicesAsync();
            };


            

            

        }

        private void Adaptador_ScanTimeoutElapsed(object sender, System.EventArgs e)
        {
            bleAdapter.StopScanningForDevicesAsync();
            Toast.MakeText(this, "No se ha encontrado nada", ToastLength.Long).Show();
        }

        private void Adaptador_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            var msg = string.Format("Device found: {0}", e.Device.Name);
            Toast.MakeText(this, msg, ToastLength.Long).Show();
        }

    }
}

