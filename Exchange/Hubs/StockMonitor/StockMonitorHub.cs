using System.Collections.Generic;
using Exchange.Domain;
using Microsoft.AspNet.SignalR;

namespace Exchange.Hubs.StockMonitor
{
    public class StockMonitorHub : Hub
    {
        private readonly StockMonitor _stockMonitor;

        public StockMonitorHub() : this(StockMonitor.Instance) { }

        public StockMonitorHub(StockMonitor stockTicker)
        {
            _stockMonitor = stockTicker;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _stockMonitor.GetAllStocks();
        }
    }
}