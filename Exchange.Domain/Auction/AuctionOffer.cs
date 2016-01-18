using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Account;

namespace Exchange.Domain.Auction
{
    public class AuctionOffer : BaseEntity
    {
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
