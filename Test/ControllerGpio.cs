using nanoFramework.WebServer;
using System;
using System.Device.Gpio;
using System.Net;

namespace Test
{
    //[Authentication("none")]
    public class ControllerHello
    {
        [Route("hello")]
        [Method("GET")]
        public void Open(WebServerEventArgs e)
        {
            try
            {
                e.Context.Response.ContentType = "text/plain";
                WebServer.OutPutStream(e.Context.Response, "Open called ESP32");
            }
            catch (Exception)
            {
                WebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.BadRequest);
            }
        }
    }

    [Authentication("ApiKey")]
    public class Controller
    {
        

        [Route("open")]
        [Method("GET")]
        public void Open(WebServerEventArgs e)
        {
            try
            {
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
