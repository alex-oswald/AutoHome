using System;
using System.Device.Gpio;
using System.Threading;

namespace Curtains.Nano
{
    public interface IHardwareService
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
    public class HardwareService : IHardwareService, IDisposable
    {
        private readonly object _lock = new();
        private GpioController _controller;
        private GpioPin _ledPin;
        private A4988 _motor;

        public void Init()
        {
            _controller = new GpioController(PinNumberingScheme.Logical);
            _ledPin = _controller.OpenPin(Configuration.LedPin, PinMode.Output);
            _motor = new A4988(Configuration.StepPin, Configuration.DirectionPin, Configuration.EnablePin, _controller);
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
                _motor.RPM = Configuration.RPM;
                _motor.Step(Configuration.OpenSteps);
                _motor.SetEnabledState(false);
            }
        }

        public void Close()
        {
            lock (_lock)
            {
                _motor.SetEnabledState(true);
                _motor.RPM = Configuration.RPM;
                _motor.Step(Configuration.CloseSteps);
                _motor.SetEnabledState(false);
            }
        }

        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
