using nanoFramework.Networking;
using nanoFramework.WebServer;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    public static class Configuration
    {
        public const string IntegrationDeviceId = "8d291f88-3b06-428c-87fb-ecd0eea44d17";
        
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

                Thread blink = new Thread(() => Blink(_controller, CancellationToken.None));
                blink.Start();



                CancellationTokenSource cs = new(60000);
                var success = WifiNetworkHelper.ConnectDhcp(Configuration.Wifi.SSID, Configuration.Wifi.Password, requiresDateTime: true, token: cs.Token);
                if (!success)
                {
                    //_controller.Write(2, PinValue.Low);
                    Debug.WriteLine($"Can't get a proper IP address and DateTime, error: {WifiNetworkHelper.Status}.");
                    if (WifiNetworkHelper.HelperException != null)
                    {
                        Debug.WriteLine($"Exception: {WifiNetworkHelper.HelperException}");
                    }
                    return;
                }

                blink.Abort();


                _controller.Write(2, PinValue.Low);
                using WebServer server = new(80, HttpProtocol.Http, new Type[] { typeof(Controller), typeof(ControllerHello) });
                server.ApiKey = Configuration.IntegrationDeviceId;
                server.Start();


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

        public static void Blink(GpioController controller, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                controller.Write(2, PinValue.High);
                Thread.Sleep(50);
                controller.Write(2, PinValue.Low);
                Thread.Sleep(50);
                controller.Write(2, PinValue.High);
                Thread.Sleep(50);
                controller.Write(2, PinValue.Low);
                Thread.Sleep(50);
                controller.Write(2, PinValue.High);
                Thread.Sleep(50);
                controller.Write(2, PinValue.Low);
                Thread.Sleep(750);
            }
        }
    }
}
