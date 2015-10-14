using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.DataAccess.DomainConfiguration;
using Exchange.Domain;

namespace EFTest
{
    public class TestContext : DbContext
    {
        public TestContext()
            : base("Exchange")
        {
            Database.SetInitializer<TestContext>(null);
        }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
