using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Auction;

namespace Exchange.Abstract.Services
{
    public interface IAuctionOfferService
    {
        IEnumerable<AuctionOffer> GetAuctionOffers(int auctionId);
        void AddAuctionOffer(AuctionOffer auctionOffer);
    }
}
