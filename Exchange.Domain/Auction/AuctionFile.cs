using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Auction
{
    public class AuctionFile : BaseEntity
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
