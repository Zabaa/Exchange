using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Auction;

namespace Exchange.Abstract.Services
{
    public interface IAuctionService
    {
        IEnumerable<Auction> GetAuctions();
        void CreateAuction(Auction auction);
        void UpdateAuction(Auction auction);
        void DeleteAuction(int id);
    }
}
