using System;
using System.Device.Gpio;
using System.Threading;

namespace AutoHome.Integrations.NanoCore
{
    public interface IStatusLight
    {
        void Init();
        void BlinkSlow(CancellationToken cancellationToken);
        void BlinkFast();
    }

    /// <summary>
    /// This class is thread safe.
    /// </summary>
    public class StatusLight : IStatusLight, IDisposable
    {
        private readonly object _lock = new();
        private readonly IHardwareController _controller;
        private GpioPin _ledPin;

        public StatusLight(IHardwareController controller)
        {
            _controller = controller;
        }

        public void Init()
        {
            _ledPin = _controller.GpioController.OpenPin(2, PinMode.Output);
        }

        public void BlinkFast()
        {
            lock (_lock)
            {
                _ledPin.Write(PinValue.High);
                Thread.Sleep(50);
                _ledPin.Write(PinValue.Low);
                Thread.Sleep(50);
                _ledPin.Write(PinValue.High);
                Thread.Sleep(50);
                _ledPin.Write(PinValue.Low);
            }
        }

        public void BlinkSlow(CancellationToken cancellationToken)
        {
            Thread blink = new(() => BlinkSlow(_ledPin, cancellationToken));
            blink.Start();
        }

        private void BlinkSlow(GpioPin pin, CancellationToken cancellationToken)
        {
            if (pin == null)
            {
                throw new ArgumentNullException(nameof(pin));
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                lock (_lock)
                {
                    pin.Write(PinValue.High);
                    Thread.Sleep(250);
                    pin.Write(PinValue.Low);
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    continue;
                }
                Thread.Sleep(1750);
            }
        }

        public void Dispose()
        {
            _ledPin.Dispose();
        }
    }
}
