using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Abstract.Services;
using Exchange.DataAccess.Context;
using Exchange.Domain.Auction;

namespace Exchange.Service
{
    public class AuctionService : IAuctionService
    {
        private readonly ExchangeContext _exchangeContext;

        public AuctionService()
        {
            _exchangeContext = new ExchangeContext();
        }

        public IEnumerable<Auction> GetAuctions()
        {
            return _exchangeContext.Auctions;
        }

        public void CreateAuction(Auction auction)
        {
            _exchangeContext.Auctions.Add(auction);
            _exchangeContext.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            var auctionU = _exchangeContext.Auctions.SingleOrDefault(a => a.Id == auction.Id);
            if (auctionU != null)
            {
                auctionU.Name = auction.Name;
                auctionU.Description = auction.Description;
                auctionU.OpenPrice = auction.OpenPrice;
                auctionU.StartDate = auction.StartDate;
                auctionU.Status = auction.Status;
                _exchangeContext.SaveChanges();
            }
        }

        public void DeleteAuction(int id)
        {
            var auctionD = _exchangeContext.Auctions.SingleOrDefault(a => a.Id ==id);
            if (auctionD != null)
            {
                _exchangeContext.Auctions.Remove(auctionD);
            }
        }
    }
}
