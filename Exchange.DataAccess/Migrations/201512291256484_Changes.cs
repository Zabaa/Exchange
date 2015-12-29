namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AuctionHistory", newName: "AuctionOffer");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AuctionOffer", newName: "AuctionHistory");
        }
    }
}
