using System;
using System.Device.Gpio;
using System.Threading;

namespace AutoHome.Integrations.NanoCore
{
    public interface IHardwareManager
    {
        void Init();
        void BlinkSlow(CancellationToken cancellationToken);
        void BlinkFast();
        void Open();
        void Close();
    }

    /// <summary>
    /// This class is thread safe.
    /// </summary>
    public class HardwareManager : IHardwareManager, IDisposable
    {
        private readonly object _lock = new();
        private readonly IIntegrationOptions _options;
        private GpioController _controller;
        private GpioPin _ledPin;
        private A4988 _motor;

        public HardwareManager(IIntegrationOptions options)
        {
            _options = options;
        }

        public void Init()
        {
            _controller = new GpioController(PinNumberingScheme.Logical);
            _ledPin = _controller.OpenPin(_options.LedPin, PinMode.Output);
            _motor = new A4988(_options.StepPin, _options.DirectionPin, _options.EnablePin, _controller);
        }

        public void BlinkSlow(CancellationToken cancellationToken)
        {
            Thread blink = new(() => BlinkSlow(_ledPin, cancellationToken));
            blink.Start();
        }

        private void BlinkSlow(GpioPin pin, CancellationToken cancellationToken)
        {
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

        public void Open()
        {
            lock (_lock)
            {
                _motor.SetEnabledState(true);
                _motor.RPM = _options.RPM;
                _motor.Step(_options.OpenSteps);
                _motor.SetEnabledState(false);
            }
        }

        public void Close()
        {
            lock (_lock)
            {
                _motor.SetEnabledState(true);
                _motor.RPM = _options.RPM;
                _motor.Step(_options.CloseSteps);
                _motor.SetEnabledState(false);
            }
        }

        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
