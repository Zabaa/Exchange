using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Auction;

namespace Exchange.ViewModel.Auction
{
    public class AuctionViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PredictedEndDate { get; set; }
        public AuctionStatus Status { get; set; }
    }
}
