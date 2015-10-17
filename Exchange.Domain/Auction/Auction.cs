using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Auction
{
    public class Auction : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PredictedEndDate { get; set; }
        public DateTime LastPriceChangeDate { get; set; }
        public int Status { get; set; }

        public virtual ICollection<AuctionFile> AuctionFiles { get; set; }
        public virtual ICollection<AuctionHistory> AuctionHistories { get; set; }
    }
}
