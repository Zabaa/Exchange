using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Auction
{
    public enum AuctionStatus
    {
        [Description("Aktywna")]
        Ready = 1,
        [Description("Rozpoczęta")]
        Started = 2,
        [Description("Zakończona")]
        Finished = 3
    }
}
