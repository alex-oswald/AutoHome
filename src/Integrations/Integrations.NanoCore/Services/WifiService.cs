using nanoFramework.Hosting;
using nanoFramework.Networking;
using System.Diagnostics;
using System.Threading;

namespace AutoHome.Integrations.NanoCore.Services
{
    public class WifiService : BackgroundService
    {
        private readonly IHardwareManager _hardwareManager;

        public WifiService(
            IHardwareManager hardwareManager)
        {
            _hardwareManager = hardwareManager;
        }

        protected override void ExecuteAsync()
        {
            Debug.WriteLine("Waiting for network up and IP address...");

        reset:
            // Start blinking the LED slowly to identify connecting to the network
            CancellationTokenSource blinkCts = new();
            _hardwareManager.BlinkSlow(blinkCts.Token);

            CancellationTokenSource connectTimeoutCts = new(60000);
            var success = WifiNetworkHelper.Reconnect(true, token: connectTimeoutCts.Token);

            if (!success)
            {
                Debug.WriteLine($"Can't get a proper IP address and DateTime, error: {WifiNetworkHelper.Status}.");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"Exception: {WifiNetworkHelper.HelperException}");
                }
                // Start over trying to connect to wifi
                Debug.WriteLine("Go back and try to connect to WiFi again");
                goto reset;
            }
            Debug.WriteLine("Connected to WiFi");

            // Stop blinking
            blinkCts.Cancel();
        }
    }
}
