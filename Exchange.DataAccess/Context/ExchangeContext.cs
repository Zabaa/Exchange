using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Exchange.DataAccess.DomainConfiguration;
using Exchange.Domain;
using Exchange.Domain.Auction;

namespace Exchange.DataAccess.Context
{
    public class ExchangeContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionFile> AuctionFiles { get; set; }
        public DbSet<AuctionOffer> AuctionOffers { get; set; }

        public ExchangeContext()
            : base("Exchange")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ExchangeContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new StockConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
