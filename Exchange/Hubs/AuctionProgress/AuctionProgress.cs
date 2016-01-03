using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exchange.Domain.Auction;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Exchange.Hubs.AuctionProgress
{
    public class AuctionProgress
    {
        private static readonly Lazy<AuctionProgress> _instance =
            new Lazy<AuctionProgress>(
                () => new AuctionProgress(GlobalHost.ConnectionManager.GetHubContext<AuctionProgressHub>().Clients));

        private IHubConnectionContext<dynamic> _clients;

        public static AuctionProgress Instance
        {
            get { return _instance.Value; }
        }

        private AuctionProgress(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public void BroadcastAuctionProgress(AuctionOffer auctionOffer)
        {
            _clients.All.addStockPrice(auctionOffer);
        }
    }
}