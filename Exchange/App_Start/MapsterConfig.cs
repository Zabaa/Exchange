using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Exchange.Controllers;
using Exchange.Domain.Auction;
using Exchange.ViewModel.Auction;
using Exchange.ViewModel.AuctionOffer;
using Mapster;

namespace Exchange.App_Start
{
    public class MapsterConfig
    {
        public static void Config()
        {
            TypeAdapterConfig.GlobalSettings.AllowImplicitDestinationInheritance = true;
            ConfigureModelMappings();
            TypeAdapterConfig.Validate();
        }

        private static void ConfigureModelMappings()
        {
            #region Auction

            TypeAdapterConfig<Auction, AuctionGridViewModel>.NewConfig()
                .Map(dest => dest.Status, src => (AuctionStatus) src.Status)
                .Map(dest => dest.Price, src => src.AuctionOffers != null && src.AuctionOffers.Any() ? src.AuctionOffers.Max(a => a.Price) : src.OpenPrice);

            TypeAdapterConfig<AuctionGridViewModel, Auction>.NewConfig()
                .Ignore(dest => dest.AuctionOffers)
                .Ignore(dest => dest.AuctionFiles)
                .Ignore(dest => dest.UserId)
                .Ignore(dest => dest.User)
                .Ignore(dest => dest.LastPriceChangeDate)
                .Map(dest => dest.Status, src => (int)src.Status);

            TypeAdapterConfig<Auction, AuctionViewModel>.NewConfig()
                .Ignore(dest => dest.UserName)
                .Map(dest => dest.Status, src => (AuctionStatus) src.Status)
                .Map(dest => dest.AuctionOffers, src => TypeAdapter.Adapt<IEnumerable<AuctionOffer>, IEnumerable<AuctionOfferViewModel>>(src.AuctionOffers));


            TypeAdapterConfig<AuctionViewModel, Auction>.NewConfig()
                .Ignore(dest => dest.AuctionOffers)
                .Ignore(dest => dest.AuctionFiles)
                .Ignore(dest => dest.LastPriceChangeDate)
                .Ignore(dest => dest.Price)
                .Ignore(dest => dest.AuctionOffers)
                .Ignore(dest => dest.User)
                .Map(dest => dest.Status, src => (int)src.Status);

            #endregion

            #region AuctionOffer

            TypeAdapterConfig<AuctionOffer, AuctionOfferViewModel>.NewConfig()
                .Map(dest => dest.UserName, src => src.User != null ? src.User.UserName : string.Empty);

            TypeAdapterConfig<AuctionOfferViewModel, AuctionOffer>.NewConfig()
                .Ignore(dest => dest.Auction)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.User);


            #endregion
        }
    }
}