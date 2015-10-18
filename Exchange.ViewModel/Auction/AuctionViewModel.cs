using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Auction;

namespace Exchange.ViewModel.Auction
{
    public class AuctionViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal OpenPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PredictedEndDate { get; set; }
        public AuctionStatus Status { get; set; }
    }
}
