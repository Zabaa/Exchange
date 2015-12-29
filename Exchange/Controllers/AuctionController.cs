using Exchange.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Abstract.Services;
using Exchange.Domain.Auction;
using Exchange.Infrastructure.Extensions;
using Exchange.ViewModel.Auction;
using Exchange.ViewModel.AuctionOffer;
using Mapster;
using Microsoft.AspNet.Identity;
using NLog;

namespace Exchange.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IAuctionOfferService _auctionOfferService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AuctionController(IAuctionService auctionService, IAuctionOfferService auctionOfferService)
        {
            _auctionService = auctionService;
            _auctionOfferService = auctionOfferService;
        }

        public ActionResult Index()
        {
            IEnumerable<Auction> auctions = _auctionService.GetAuctions(User.Identity.GetUserId()).ToList();
            var viewModel = TypeAdapter.Adapt<IEnumerable<Auction>, IEnumerable<AuctionGridViewModel>>(auctions);

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult StartedAuctions()
        {
            IEnumerable<Auction> auctions = _auctionService.GetStartedAuctions();
            var viewModel = TypeAdapter.Adapt<IEnumerable<Auction>, IEnumerable<AuctionGridViewModel>>(auctions);

            return View(viewModel);
        }

        public ActionResult AuctionProgress(int id)
        {
            var auction = _auctionService.GetAuction(id);
            var viewModel = TypeAdapter.Adapt<Auction, AuctionViewModel>(auction);
            viewModel.UserName = User.Identity.Name;

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var auction = _auctionService.GetAuction(id);
            var viewModel = TypeAdapter.Adapt<Auction, AuctionViewModel>(auction);

            return PartialView("_Details", viewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new AuctionViewModel {StartDate = DateTime.Now, PredictedEndDate = DateTime.Now};
            ViewData["AuctionStatuses"] = GetAuctionStatuses();
            return PartialView("_Add", viewModel);
        }

        [HttpPost]
        public ActionResult Add(AuctionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return PartialView("_Add", viewModel);

            var auction = TypeAdapter.Adapt<AuctionViewModel, Auction>(viewModel);
            auction.LastPriceChangeDate = auction.StartDate;
            auction.UserId = User.Identity.GetUserId();

            try
            {
                _auctionService.AddAuction(auction);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var auction = _auctionService.GetAuction(id);
            var viewModel = TypeAdapter.Adapt<Auction, AuctionViewModel>(auction);
            ViewData["AuctionStatuses"] = GetAuctionStatuses();
            return PartialView("_Update", viewModel);
        }

        [HttpPost]
        public ActionResult Update(AuctionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return PartialView("_Update", viewModel);

            var auction = TypeAdapter.Adapt<AuctionViewModel, Auction>(viewModel);
            auction.LastPriceChangeDate = auction.StartDate;
            auction.UserId = User.Identity.GetUserId();

            try
            {
                _auctionService.UpdateAuction(auction);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult AddOffer(AuctionOfferViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            var auctionOffer = TypeAdapter.Adapt<AuctionOfferViewModel, AuctionOffer>(viewModel);
            
            try
            {
                _auctionOfferService.AddAuctionOffer(auctionOffer);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new { success = false });
            }
        }

        private IEnumerable<SelectListItem> GetAuctionStatuses()
        {
            return Enum.GetValues(typeof(AuctionStatus)).Cast<AuctionStatus>().Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            }).ToList();
        }
    }
}