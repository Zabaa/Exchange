using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Exchange.Hubs.AuctionProgress
{
    [Authorize]
    public class AuctionProgressHub : Hub
    {
        private readonly AuctionProgress _auctionProgress;

        public AuctionProgressHub() : this(AuctionProgress.Instance)
        {
            
        }

        public AuctionProgressHub(AuctionProgress auctionProgress)
        {
            _auctionProgress = auctionProgress;
        }
    }
}