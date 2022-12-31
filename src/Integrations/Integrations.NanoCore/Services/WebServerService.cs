using nanoFramework.DependencyInjection;
using nanoFramework.Hosting;
using nanoFramework.WebServer;
using System;
using System.Diagnostics;
using System.Net;

namespace AutoHome.Integrations.NanoCore.Services
{
    public class WebServerService<TController1, TController2> : BackgroundService
    {
        private readonly IServiceProvider _sp;
        private readonly IIntegrationOptions _options;

        public WebServerService(
            IServiceProvider sp,
            IIntegrationOptions options)
        {
            _sp = sp;
            _options = options;
        }

        protected override void ExecuteAsync()
        {
            // Setup web server
            using WebServerDi server = new(80, HttpProtocol.Http, new Type[] { typeof(TController1), typeof(TController2) }, _sp);
            server.ApiKey = _options.IntegrationDeviceId;
            server.Start();
            Debug.WriteLine("Web server started");
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
