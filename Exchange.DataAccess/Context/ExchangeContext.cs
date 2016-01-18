using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Exchange.DataAccess.DomainConfiguration;
using Exchange.Domain;
using Exchange.Domain.Account;
using Exchange.Domain.Auction;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Exchange.DataAccess.Context
{
    public class ExchangeContext : IdentityDbContext<ApplicationUser>
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

        public static ExchangeContext Create()
        {
            return new ExchangeContext();
        }
    }
}
