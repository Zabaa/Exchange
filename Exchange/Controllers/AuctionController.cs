using Exchange.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exchange.Controllers
{
    public class AuctionController : Controller
    {
        // GET: Auction
        public ActionResult Index()
        {
            var dbContext = new ExchangeContext();
            var stocks = dbContext.Stocks;
            foreach (var stock in stocks)
            {
                System.Diagnostics.Debug.WriteLine("{0} - {1}", stock.Symbol, stock.Price);
            }
            return View();
        }

        public ActionResult Auction()
        {
            return View();
        }
    }
}