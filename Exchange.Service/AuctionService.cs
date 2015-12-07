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

        public IEnumerable<Auction> GetAuctions(string userId)
        {
            return _exchangeContext.Auctions.Where(a => a.UserId == userId);
        }

        public Auction GetAuction(int id)
        {
            return _exchangeContext.Auctions.SingleOrDefault(a => a.Id == id);
        }

        public void CreateAuction(Auction auction)
        {
            _exchangeContext.Auctions.Add(auction);
            _exchangeContext.SaveChanges();
        }

        public void UpdateAuction(Auction auction)
        {
            var auctionToUpdate = _exchangeContext.Auctions.SingleOrDefault(a => a.Id == auction.Id);
            if (auctionToUpdate != null)
            {
                auctionToUpdate.Name = auction.Name;
                auctionToUpdate.Description = auction.Description;
                auctionToUpdate.OpenPrice = auction.OpenPrice;
                auctionToUpdate.StartDate = auction.StartDate;
                auctionToUpdate.Status = auction.Status;
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

        public IEnumerable<Auction> GetStartedAuctions()
        {
            return _exchangeContext.Auctions.Where(a => a.Status == (int) AuctionStatus.Started);   
        }
    }
}
