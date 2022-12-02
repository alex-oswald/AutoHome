using nanoFramework.M2Mqtt;
using nanoFramework.Networking;
using nanoFramework.WebServer;
using System;
using System.Device.Gpio;
using System.Device.Wifi;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Test
{
    public static class Configuration
    {
        public const string IntegrationDeviceId = "8d291f88-3b06-428c-87fb-ecd0eea44d17";
        public const string MqttBroker = "pi.lan";

        public static class Wifi
        {
            public const string SSID = "";
            public const string Password = "";
        }
    }

    public class Program
    {
        private static GpioController _controller = new GpioController(PinNumberingScheme.Logical);

        public static void Main()
        {
            try
            {
                Debug.WriteLine("Waiting for network up and IP address...");

                _controller.OpenPin(2, PinMode.Output);
                _controller.Write(2, PinValue.Low);

                Thread blink = new Thread(() => BlinkSlow(_controller, CancellationToken.None));
                blink.Start();

            wifi_connect:
                CancellationTokenSource cs = new(60000);
                WifiNetworkHelper.SetupNetworkHelper(true);
                var success = WifiNetworkHelper.ConnectDhcp(Configuration.Wifi.SSID, Configuration.Wifi.Password, WifiReconnectionKind.Automatic, requiresDateTime: true, token: cs.Token);
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
                blink.Abort();

                // Setup web server
                _controller.Write(2, PinValue.Low);
                using WebServer server = new(80, HttpProtocol.Http, new Type[] { typeof(Controller), typeof(ControllerHello) });
                server.ApiKey = Configuration.IntegrationDeviceId;
                server.Start();
                Debug.WriteLine("Web server started");

                // Setup MQTT
                MqttClient client = new MqttClient("pi.lan", 1883, false, null, null, MqttSslProtocols.None);
                client.ProtocolVersion = MqttProtocolVersion.Version_5;
                client.Connect(Configuration.IntegrationDeviceId, true);
                client.Publish("device/test/status", Encoding.UTF8.GetBytes("Device is online"));

                while (true)
                {
                    Thread.Sleep(30000);
                    BlinkHigh(_controller, CancellationToken.None);
                    client.Publish("device/test/status", Encoding.UTF8.GetBytes("heartbeat"));
                }

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex}");
            }
            finally
            {

            }
        }

        public static void BlinkSlow(GpioController controller, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                controller.Write(2, PinValue.High);
                Thread.Sleep(250);
                controller.Write(2, PinValue.Low);
                Thread.Sleep(1750);
            }
        }

        public static void BlinkHigh(GpioController controller, CancellationToken cancellationToken)
        {
            controller.Write(2, PinValue.High);
            Thread.Sleep(50);
            controller.Write(2, PinValue.Low);
            Thread.Sleep(50);
            controller.Write(2, PinValue.High);
            Thread.Sleep(50);
            controller.Write(2, PinValue.Low);
        }
    }
}
