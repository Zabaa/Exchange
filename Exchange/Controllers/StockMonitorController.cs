using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Hubs.StockMonitor;
using Exchange.Service;

namespace Exchange.Controllers
{
    public class StockMonitorController : Controller
    {
        private readonly StockService _stockService;
        private readonly StockMonitor _stockMonitor;

        public StockMonitorController()
        {
            _stockService = new StockService();
            _stockMonitor = StockMonitor.Instance;
        }

        // GET: StockMonitor
        public ActionResult Index()
        {
            return View();
        }
    }
}