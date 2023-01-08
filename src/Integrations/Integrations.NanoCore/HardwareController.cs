using System.Device.Gpio;
using System;

namespace AutoHome.Integrations.NanoCore
{
    public interface IHardwareController : IDisposable
    {
        GpioController GpioController { get; }
    }

    public class HardwareController : IHardwareController
    {
        private readonly GpioController _controller;

        public HardwareController()
        {
            _controller = new GpioController(PinNumberingScheme.Logical);
        }

        public HardwareController(GpioController controller)
        {
            _controller = controller;
        }

        public GpioController GpioController => _controller;

        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
