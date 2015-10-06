namespace Exchange.DataAccess.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Exchange.DataAccess.Context.ExchangeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Exchange.DataAccess.Context.ExchangeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Stocks.AddOrUpdate(
                new Stock { Symbol = "Firma1", Price = 663.31m, LastChangeDate = DateTime.Now },
                new Stock { Symbol = "Firma2", Price = 665.18m, LastChangeDate = DateTime.Now },
                new Stock { Symbol = "Firma3", Price = 664.30m, LastChangeDate = DateTime.Now },
                new Stock { Symbol = "Firma4", Price = 663.98m, LastChangeDate = DateTime.Now },
                new Stock { Symbol = "Firma5", Price = 667.57m, LastChangeDate = DateTime.Now }
                );
        }
    }
}
