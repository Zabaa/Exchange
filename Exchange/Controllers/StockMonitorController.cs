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
        // GET: StockMonitor
        public ActionResult Index()
        {
            return View();
        }
    }
}