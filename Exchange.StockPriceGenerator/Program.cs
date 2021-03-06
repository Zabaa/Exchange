﻿using System.ServiceProcess;

namespace Exchange.StockPriceGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1() { ServiceName = "StockPriceGenerator" }
            };

            WindowsServiceInvoker.Invoke(ServicesToRun, args);
            ServiceBase.Run(ServicesToRun);
        }
    }
}
