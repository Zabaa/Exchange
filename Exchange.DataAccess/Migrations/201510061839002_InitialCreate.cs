namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(nullable: false, maxLength: 32, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"),
                        DayOpen = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        LastChangeDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stock");
        }
    }
}
