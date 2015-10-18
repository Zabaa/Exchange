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

namespace Exchange.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: Auction
        public ActionResult Index()
        {
            var auctions = _auctionService.GetAuctions(User.Identity.GetUserId()).ToList();
            var viewModel = TypeAdapter.Adapt<IEnumerable<Auction>, IEnumerable<AuctionViewModel>>(auctions);

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
    }
}