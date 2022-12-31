using AutoHome.Integrations.NanoCore;
using nanoFramework.WebServer;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace AutoHome.Integrations.Curtains.Nano
{
    public class HealthController
    {
        [Route("health")]
        [Method("GET")]
        public void Open(WebServerEventArgs e)
        {
            try
            {
                Debug.WriteLine("Health endpoint");
                e.Context.Response.ContentType = "text/plain";
                WebServer.OutPutStream(e.Context.Response, "healthy");
            }
            catch (Exception)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }
        }
    }

    [Authentication("ApiKey")]
    public class ControlController
    {
        private readonly IHardwareManager _hardware;
        private readonly IMessagingManager _messaging;

        public ControlController(IHardwareManager hardware, IMessagingManager messaging)
        {
            _hardware = hardware;
            _messaging = messaging;
        }

        [Route("open")]
        [Method("GET")]
        public void Open(WebServerEventArgs e)
        {
            try
            {
                Debug.WriteLine("Open command received");
                var request = e.Context.Request;
                var headers = request.Headers.AllKeys;
                _messaging.PublishStatus("Open command received, opening...");
                Thread thread = new(() =>
                {
                    _hardware.BlinkFast();
                    _hardware.Open();
                    _hardware.BlinkFast();
                    _messaging.PublishStatus("Open complete");
                });
                thread.Start();
                e.Context.Response.ContentType = "text/plain";
                WebServer.OutPutStream(e.Context.Response, "Open called ESP32");
            }
            catch (Exception)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }
        }

        [Route("close")]
        [Method("GET")]
        public void Close(WebServerEventArgs e)
        {
            try
            {
                Debug.WriteLine("Close command received");
                _messaging.PublishStatus("Close command received, closing...");
                Thread thread = new(() =>
                {
                    _hardware.BlinkFast();
                    _hardware.Close();
                    _hardware.BlinkFast();
                    _messaging.PublishStatus("Close complete");
                });
                thread.Start();
                e.Context.Response.ContentType = "text/plain";
                WebServer.OutPutStream(e.Context.Response, "Close called ESP32");
            }
            catch (Exception)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }
        }
    }
}
