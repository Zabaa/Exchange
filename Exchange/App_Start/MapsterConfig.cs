using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Exchange.Controllers;
using Exchange.Domain.Auction;
using Exchange.ViewModel.Auction;
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
                .Map(dest => dest.Status, src => (AuctionStatus)src.Status);

            TypeAdapterConfig<AuctionGridViewModel, Auction>.NewConfig()
                .Ignore(dest => dest.AuctionHistories)
                .Ignore(dest => dest.AuctionFiles)
                .Ignore(dest => dest.UserId)
                .Ignore(dest => dest.LastPriceChangeDate)
                .Map(dest => dest.Status, src => (int)src.Status);

            TypeAdapterConfig<Auction, AuctionViewModel>.NewConfig()
                .Map(dest => dest.Status, src => (AuctionStatus)src.Status);

            TypeAdapterConfig<AuctionViewModel, Auction>.NewConfig()
                .Ignore(dest => dest.AuctionHistories)
                .Ignore(dest => dest.AuctionFiles)
                .Ignore(dest => dest.UserId)
                .Ignore(dest => dest.LastPriceChangeDate)
                .Ignore(dest => dest.Price)
                .Map(dest => dest.Status, src => (int)src.Status);


            #endregion
        }
    }
}