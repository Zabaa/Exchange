using System.ServiceProcess;

namespace Exchange.StockPriceGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1() { ServiceName = "StockPriceGenerator" }
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
