using nanoFramework.WebServer;
using System;
using System.Net;

namespace Curtains.Nano
{
    //[Authentication("none")]
    public class HelloController
    {
        [Route("")]
        [Method("GET")]
        public void Open(WebServerEventArgs e)
        {
            try
            {
                e.Context.Response.ContentType = "text/plain";
                WebServer.OutPutStream(e.Context.Response, $"AutoHome Device - {Configuration.DeviceName}");
            }
            catch (Exception)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }
        }
    }

    //[Authentication("ApiKey")]
    public class ControlController
    {
        private readonly IHardwareService _hardware;
        private readonly IMessaging _messaging;

        public ControlController(IHardwareService hardware, IMessaging messaging)
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
                _messaging.PublishStatus("Open command received, opening...");
                _hardware.BlinkFast();
                _hardware.Open();
                _hardware.BlinkFast();
                _messaging.PublishStatus("Open complete");
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
                _messaging.PublishStatus("Close command received, closing...");
                _hardware.BlinkFast();
                _hardware.Close();
                _hardware.BlinkFast();
                _messaging.PublishStatus("Close complete");
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
