using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.StockPriceGenerator
{
    public class WindowsServiceInvoker
    {

        public static void Invoke(ServiceBase[] services, string[] args)
        {
            var serviceNames = GetNames(services);

            if (Environment.UserInteractive)
            {
                var onStart = typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);

                try
                {
                    Console.WriteLine("Starting Service {0}", serviceNames);

                    foreach (var service in services)
                    {
                        onStart.Invoke(service, new object[] { args });
                    }

                    Console.WriteLine("Service started");
                    Console.WriteLine("");
                    Console.ReadKey(true);
                    Console.WriteLine("Stoping services");

                    foreach (var service in services)
                    {
                        service.Stop();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Terminate due the error");
                    Console.WriteLine("Press any key to ");
                    Console.ReadKey(true);
                }
            }
            else
                ServiceBase.Run(services);
        }

        private static string GetNames(ServiceBase[] services)
        {
            var names = string.Empty;

            foreach (var service in services)
            {
                names = string.IsNullOrEmpty(names) ? names : names + ", ";
                names += service.GetType().Name;
            }

            return names;
        }
    }
}
