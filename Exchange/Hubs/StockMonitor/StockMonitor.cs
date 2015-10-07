using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exchange.Domain;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading;
using Exchange.Service;
using TableDependency.SqlClient;
using System.Configuration;
using TableDependency.EventArgs;
using TableDependency.Enums;
using Exchange.Infrastructure;

namespace Exchange.Hubs.StockMonitor
{
    public class StockMonitor : IDisposable
    {
        private static readonly Lazy<StockMonitor> _instance =
            new Lazy<StockMonitor>(
                () => new StockMonitor(GlobalHost.ConnectionManager.GetHubContext<StockMonitorHub>().Clients));

        private readonly ConcurrentDictionary<string, Stock> _stocks = new ConcurrentDictionary<string, Stock>();

        private readonly object _updateStockPricesLock = new object();

        //stock can go up or down by a percentage of this factor on each change
        private const double RangePercent = .002;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
        private readonly Random _updateOrNotRandom = new Random();

        private volatile bool _updatingStockPrices = false;

        private readonly StockService _stockService;

        private static SqlTableDependency<Stock> _tableDependency;

        private StockMonitor(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            _stockService = new StockService();
            //_stockService.StockChanged += (sender, args) =>
            //{
            //    var stocks = _stockService.GetStocks();
            //    _stocks.Clear();
            //    foreach (var stock in stocks)
            //    {
            //        _stocks.TryAdd(stock.Symbol, stock);
            //    }
            //    BroadcastStocksPrice(_stocks.Values);
            //};

            string connectionString = ExchangeConfiguration.ConnectionString;

            _tableDependency = new SqlTableDependency<Stock>(connectionString, "Stock");
            _tableDependency.OnChanged += StockTableChanged;
            _tableDependency.OnError += _tableDependency_OnError;
            _tableDependency.Start();
        }

        private void _tableDependency_OnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StockTableChanged(object sender, RecordChangedEventArgs<Stock> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                BroadcastStockPrice(e.Entity);
            }
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

        private void BroadcastStocksPrice(IEnumerable<Stock> stocks)
        {
            Clients.All.updateStocksPrice(stocks);
        }

        private void UpdateStockPrices(object state)
        {
            lock (_updateStockPricesLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    foreach (var stock in _stocks.Values)
                    {
                        if (TryUpdateStockPrice(stock))
                        {
                            BroadcastStockPrice(stock);
                        }
                    }

                    _updatingStockPrices = false;
                }
            }
        }

        private bool TryUpdateStockPrice(Stock stock)
        {
            // Randomly choose whether to update this stock or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > .1)
            {
                return false;
            }

            // Update the stock price by a random factor of the range percent
            var random = new Random((int)Math.Floor(stock.Price));
            var percentChange = random.NextDouble() * RangePercent;
            var pos = random.NextDouble() > .51;
            var change = Math.Round(stock.Price * (decimal)percentChange, 2);
            change = pos ? change : -change;

            stock.Price += change;
            return true;
        }

        private void BroadcastStockPrice(Stock stock)
        {
            Clients.All.updateStockPrice(stock);
        }

        #region Dispose

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
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