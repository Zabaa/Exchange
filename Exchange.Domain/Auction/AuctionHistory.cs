using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Auction
{
    public class AuctionHistory : BaseEntity
    {
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }

        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
