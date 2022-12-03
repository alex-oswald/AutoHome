using System;
using System.Device.Gpio;
using System.Threading;

namespace Curtains.Nano
{
    public interface IHardwareService
    {
        void BlinkSlow(CancellationToken cancellationToken);
        void BlinkFast();
        void Open();
        void Close();
    }

    public class HardwareService : IHardwareService, IDisposable
    {
        private readonly GpioController _controller;
        private readonly GpioPin _ledPin;
        private readonly A4988 _motor;

        public HardwareService()
        {
            _controller = new GpioController(PinNumberingScheme.Logical);
            _ledPin = _controller.OpenPin(Configuration.LedPin, PinMode.Output);
            _motor = new A4988(Configuration.StepPin, Configuration.DirectionPin, Configuration.EnablePin, _controller);
        }

        public void BlinkSlow(CancellationToken cancellationToken)
        {
            Thread blink = new(() => blinkSlow(_ledPin, cancellationToken));
            blink.Start();

            static void blinkSlow(GpioPin pin, CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    pin.Write(PinValue.High);
                    Thread.Sleep(250);
                    pin.Write(PinValue.Low);
                    Thread.Sleep(1750);
                }
            }
        }

        public void BlinkFast()
        {
            _ledPin.Write(PinValue.High);
            Thread.Sleep(50);
            _ledPin.Write(PinValue.Low);
            Thread.Sleep(50);
            _ledPin.Write(PinValue.High);
            Thread.Sleep(50);
            _ledPin.Write(PinValue.Low);
        }

        public void Open()
        {
            _motor.SetEnabledState(true);
            _motor.RPM = Configuration.RPM;
            _motor.Step(Configuration.OpenSteps);
            _motor.SetEnabledState(false);
        }

        public void Close()
        {
            _motor.SetEnabledState(true);
            _motor.RPM = Configuration.RPM;
            _motor.Step(Configuration.CloseSteps);
            _motor.SetEnabledState(false);
        }

        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
