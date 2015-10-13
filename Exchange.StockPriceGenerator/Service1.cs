﻿using Exchange.Abstract.Services;
using Exchange.Domain;
using Ninject;
using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace Exchange.StockPriceGenerator
{
    public partial class Service1 : ServiceBase
    {
        private EventLog eventLog;
        private readonly object _lockObject = new object();
        private readonly TimeSpan _timerInterval = TimeSpan.FromMilliseconds(5000);
        private readonly Random _updateOrNotRandom = new Random();
        private volatile bool _updatingStockPrice = false;
        private const double RangePercent = .002;

        private readonly IStockService _stockService;

        public Service1()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _stockService = kernel.Get<IStockService>(); ;

            eventLog = new EventLog();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog.Source = "MySource";
            eventLog.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Start StockPriceGenerator service");
            var timer = new Timer(SetRandomStockPrice, null, _timerInterval, _timerInterval);
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stop StockPriceGenerator service");
        }

        private void SetRandomStockPrice(object state)
        {
            lock (_lockObject)
            {
                if (!_updatingStockPrice)
                {
                    _updatingStockPrice = true;
                    //Todo: service.getstock -> tryupdateMethod -> service.update
                    var stocks = _stockService.GetStocks();
                    foreach (var stock in stocks)
                    {
                        if (TryUpdateStockPrice(stock))
                        {
                            _stockService.UpdateStockPrice(stock.Id, stock.Price);
                        }
                    }

                    _updatingStockPrice = false;
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
    }
}
