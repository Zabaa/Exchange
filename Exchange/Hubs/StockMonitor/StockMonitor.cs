using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Exchange.Domain;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Exchange.Service;
using Exchange.Infrastructure;
using Exchange.Infrastructure.Exceptions;
using TableDependency.SqlClient;
using TableDependency.EventArgs;
using TableDependency.Enums;

namespace Exchange.Hubs.StockMonitor
{
    public class StockMonitor : IDisposable
    {
        private static readonly Lazy<StockMonitor> _instance =
            new Lazy<StockMonitor>(
                () => new StockMonitor(GlobalHost.ConnectionManager.GetHubContext<StockMonitorHub>().Clients));

        private readonly ConcurrentDictionary<string, Stock> _stocks = new ConcurrentDictionary<string, Stock>();


        private readonly StockService _stockService;
        private static SqlTableDependency<Stock> _stockDependency;

        private StockMonitor(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            _stockService = new StockService();

            _stockDependency = new SqlTableDependency<Stock>(ExchangeConfiguration.ConnectionString, "Stock");
            _stockDependency.OnChanged += StockDependencyOnChanged;
            _stockDependency.OnError += TableDependencyOnError;
            _stockDependency.Start();
        }

        private void StockDependencyOnChanged(object sender, RecordChangedEventArgs<Stock> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                BroadcastStockPrice(e.Entity);
            }
        }

        private void TableDependencyOnError(object sender, ErrorEventArgs e)
        {
            throw new TableDependencyException(e.Message, e.Error);
        }

        public static StockMonitor Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            if (!_stocks.Any())
            {
                var stocks = _stockService.GetStocks();
                foreach (var stock in stocks)
                {
                    _stocks.TryAdd(stock.Symbol, stock);
                }
            }

            return _stocks.Values;
        }

        private void BroadcastStockPrice(Stock stock)
        {
            Clients.All.updateStockPrice(stock);
        }

        #region Dispose

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _stockDependency.Stop();
                }

                disposedValue = true;
            }
        }

        ~StockMonitor()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}