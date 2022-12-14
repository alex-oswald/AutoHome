using nanoFramework.DependencyInjection;
using nanoFramework.Networking;
using System;
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
        reset:
            try
            {
                Debug.WriteLine("Waiting for network up and IP address...");

                // Start blinking the LED slowly to identify connecting to the network
                CancellationTokenSource blinkCts = new();
                var hardware = new HardwareService();
                hardware.Init();
                hardware.BlinkSlow(blinkCts.Token);

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
                // Wait as long as the slow blink takes, otherwise the hardware class could get disposed before the blink finishes
                // and throw a disposed exception
                Thread.Sleep(2000);
                hardware.BlinkFast();
                hardware.Dispose();

                // We're connected to the network now so lets run the application
                ServiceProvider services = ConfigureServices();
                var application = (Application)services.GetRequiredService(typeof(Application));
                application.Run();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR\n\n" + ex.ToString());
            }
            // If something catastrophic happens try to reset
            goto reset;
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
