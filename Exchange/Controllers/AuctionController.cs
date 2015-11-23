using Exchange.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Abstract.Services;
using Exchange.Domain.Auction;
using Exchange.ViewModel.Auction;
using Mapster;
using Microsoft.AspNet.Identity;
using NLog;

namespace Exchange.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public ActionResult Index()
        {
            var auctions = _auctionService.GetAuctions(User.Identity.GetUserId()).ToList();
            var viewModel = TypeAdapter.Adapt<IEnumerable<Auction>, IEnumerable<AuctionGridViewModel>>(auctions);

            return View(viewModel);
        }

        public ActionResult Auction()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var auction = _auctionService.GetAuction(id);
            var viewModel = TypeAdapter.Adapt<Auction, AuctionViewModel>(auction);

            return PartialView("_Details", viewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new AuctionViewModel();
            return PartialView("_Add", viewModel);
        }

        [HttpPost]
        public ActionResult Add(AuctionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return PartialView("_Add", viewModel);

            if (viewModel.StartDate == default(DateTime))
                viewModel.StartDate = DateTime.Now;

            var auction = TypeAdapter.Adapt<AuctionViewModel, Auction>(viewModel);
            auction.LastPriceChangeDate = auction.StartDate;
            auction.UserId = User.Identity.GetUserId();

            try
            {
                _auctionService.CreateAuction(auction);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new { success = false });
            }
        }
    }
}