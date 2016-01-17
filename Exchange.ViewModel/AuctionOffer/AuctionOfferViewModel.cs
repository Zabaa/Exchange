using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.ViewModel.AuctionOffer
{
    public class AuctionOfferViewModel
    {
        [Required]
        public int AuctionId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Price { get; set; }
    }
}
