using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Exchange.Controllers;
using Exchange.DataAccess.Migrations;
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
            TypeAdapterConfig<Domain.Auction.Auction, AuctionViewModel>.NewConfig()
                .Map(dest => dest.Status, src => (AuctionStatus) src.Status);

            TypeAdapterConfig<AuctionViewModel, Domain.Auction.Auction>.NewConfig()
                .Ignore(dest => dest.AuctionHistories)
                .Ignore(dest => dest.AuctionFiles)
                .Map(dest => dest.Status, src => (int) src.Status);
        }
    }
}