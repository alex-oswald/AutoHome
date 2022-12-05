using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace Curtains.Nano
{
    public class A4988
    {
        public short StepsPerRevolution = 200;
        private readonly Stopwatch _stopwatch = new();
        private int _steps = 0;
        private int _rpm = 1;
        private readonly GpioPin _stepPin;
        private readonly GpioPin _directionPin;
        private readonly GpioPin _enablePin;

        public A4988(
            short stepPin,
            short directionPin,
            short enablePin,
            GpioController gpioController)
        {
            _stepPin = gpioController.OpenPin(stepPin, PinMode.Output);
            _directionPin = gpioController.OpenPin(directionPin, PinMode.Output);
            _enablePin = gpioController.OpenPin(enablePin, PinMode.Output);
        }

        public int RPM
        {
            get => _rpm;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _rpm = value;
            }
        }

        public void Step(int steps)
        {
            double lastStepTime = 0;
            _stopwatch.Restart();
            var clockwise = steps >= 0;
            _steps = Math.Abs(steps);
            var currentStep = 0;
            var stepDelayMicroseconds = 60 * 1000 * 1000 / StepsPerRevolution / RPM;

            _directionPin.Write(clockwise ? PinValue.High : PinValue.Low);

            while (currentStep < _steps)
            {
                var elapsedMicroseconds = _stopwatch.Elapsed.TotalMilliseconds * 1000;

                if (elapsedMicroseconds - lastStepTime >= stepDelayMicroseconds)
                {
                    lastStepTime = elapsedMicroseconds;

                    _stepPin.Write(PinValue.High);
                    Thread.Sleep(TimeSpan.FromTicks(50)); // 5us
                    _stepPin.Write(PinValue.Low);

                    currentStep++;
                }
            }

            _stopwatch.Stop();
        }

        /// <summary>
        /// Gets the enabled state.
        /// If the enabled pin is logic high, outputs are disabled.
        /// If the enabled pin is logic low, outputs are enabled.
        /// </summary>
        public bool GetEnabledState()
        {
            return _enablePin.Read() == PinValue.Low;
        }

        public void SetEnabledState(bool enabled)
        {
            _enablePin.Write(enabled ? PinValue.Low : PinValue.High);
        }
    }
}
