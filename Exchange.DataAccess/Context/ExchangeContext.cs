using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.DataAccess.DomainConfiguration;
using Exchange.Domain;

namespace Exchange.DataAccess.Context
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext()
            : base("Exchange")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ExchangeContext>());
        }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new StockConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
