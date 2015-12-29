using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Domain.Auction;
using Exchange.ViewModel.AuctionOffer;

namespace Exchange.ViewModel.Auction
{
    public class AuctionViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Cena początkowa")]
        public decimal OpenPrice { get; set; }

        [Required]
        [DisplayName("Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("Data zakończenia")]
        public DateTime PredictedEndDate { get; set; }

        [Required]
        [DisplayName("Status")]
        public AuctionStatus Status { get; set; }

        public string UserId { get; set; }

        [DisplayName("Nazwa użytkownika")]
        public string UserName { get; set; }

        public IEnumerable<AuctionOfferViewModel> AuctionOffers { get; set; }
    }
}
