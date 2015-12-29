﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Abstract.Services;
using Exchange.DataAccess.Context;
using Exchange.Domain.Auction;

namespace Exchange.Service
{
    public class AuctionOfferService : IAuctionOfferService
    {
        private readonly ExchangeContext _exchangeContext;

        public AuctionOfferService()
        {
            _exchangeContext = new ExchangeContext();
        }

        public IEnumerable<AuctionOffer> GetAuctionOffers(int auctionId)
        {
            return _exchangeContext.AuctionOffers.Where(a => a.AuctionId == auctionId);
        }

        public void AddAuctionOffer(AuctionOffer auctionOffer)
        {
            _exchangeContext.AuctionOffers.Add(auctionOffer);
            _exchangeContext.SaveChanges();
        }
    }
}