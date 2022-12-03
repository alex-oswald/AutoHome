using nanoFramework.DependencyInjection;
using nanoFramework.Networking;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace Curtains.Nano
{
    public static class Configuration
    {
        public const string IntegrationDeviceId = "8d291f88-3b06-428c-87fb-ecd0eea44d17";
        public const string MqttBroker = "pi.lan";
        public const string DeviceName = "test";
        public const string MqttTopicBase = "device/" + DeviceName;
        public const string MqttTopicStatus = MqttTopicBase + "/status";
        public const int HeartbeatMilliseconds = 30000;

        public const short LedPin = 2;
        public const short StepPin = 16;
        public const short DirectionPin = 17;
        public const short EnablePin = 18;

        public const short RPM = 20;
        public const short OpenSteps = 200;
        public const short CloseSteps = -200;
    }

    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Waiting for network up and IP address...");

            // Start blinking the LED slowly to identify connecting to the network
            CancellationTokenSource blinkCts = new();
            var _hardware = new HardwareService();
            _hardware.BlinkSlow(blinkCts.Token);

        wifi_connect:
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
                goto wifi_connect;
            }
            Debug.WriteLine("Connected to WiFi");

            // Stop blinking
            blinkCts.Cancel();
            _hardware.Dispose();

            ServiceProvider services = ConfigureServices();
            var application = (Application)services.GetRequiredService(typeof(Application));
            application.Run();
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(typeof(Application))
                .AddSingleton(typeof(IHardwareService), typeof(HardwareService))
                .AddSingleton(typeof(IMessaging), typeof(Messaging))
                .BuildServiceProvider();
        }
    }
}
