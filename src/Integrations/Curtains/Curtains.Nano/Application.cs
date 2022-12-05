using nanoFramework.DependencyInjection;
using nanoFramework.WebServer;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace Curtains.Nano
{
    public class Application
    {
        private readonly IServiceProvider _provider;
        private readonly IHardwareService _hardware;
        private readonly IMessaging _messaging;

        public Application(IServiceProvider provider, IHardwareService hardware, IMessaging messaging)
        {
            _provider = provider;
            _hardware = hardware;
            _messaging = messaging;
        }

        public void Run()
        {
            // Setup web server
            using WebServerDi server = new(80, HttpProtocol.Http, new Type[] { typeof(ControlController), typeof(HelloController) }, _provider);
            // This wasn't working. Had to add the ApiKey to the authorization attribute
            server.ApiKey = Configuration.IntegrationDeviceId;
            server.Start();
            Debug.WriteLine("Web server started");

            // Send a heartbeat message every 30 seconds
            while (true)
            {
                Thread.Sleep(Configuration.HeartbeatMilliseconds);
                Debug.WriteLine("Sending heartbeat");
                _hardware.BlinkFast();
                _messaging.Publish("status", "heartbeat");
            }
        }
    }

    internal class WebServerDi : WebServer
    {
        private readonly IServiceProvider _serviceProvider;

        public WebServerDi(int port, HttpProtocol protocol, Type[] controllers, IServiceProvider serviceProvider) : base(port, protocol, controllers)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void InvokeRoute(CallbackRoutes route, HttpListenerContext context)
        {
            route.Callback.Invoke(ActivatorUtilities.CreateInstance(_serviceProvider, route.Callback.DeclaringType), new object[] { new WebServerEventArgs(context) });
        }
    }
}
