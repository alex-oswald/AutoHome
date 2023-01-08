using AutoHome.Integrations.NanoCore;
using System.Threading;

namespace AutoHome.Integrations.Test.Nano
{
    public class Program
    {
        public static void Main()
        {
            var source = new CancellationTokenSource();

            IHardwareController controller = new HardwareController();
            IStatusLight hardware = new StatusLight(controller);

            hardware.Init();
            //hardware.BlinkSlow(source.Token);
            while (true)
            {
                hardware.BlinkFast();
                Thread.Sleep(500);
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
