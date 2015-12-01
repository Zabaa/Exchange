namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionFile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        File = c.Binary(),
                        AuctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auction", t => t.AuctionId, cascadeDelete: true)
                .Index(t => t.AuctionId);
            
            CreateTable(
                "dbo.Auction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OpenPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        PredictedEndDate = c.DateTime(nullable: false),
                        LastPriceChangeDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuctionHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuctionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auction", t => t.AuctionId, cascadeDelete: true)
                .Index(t => t.AuctionId);
            
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
            DropForeignKey("dbo.AuctionHistory", "AuctionId", "dbo.Auction");
            DropForeignKey("dbo.AuctionFile", "AuctionId", "dbo.Auction");
            DropIndex("dbo.AuctionHistory", new[] { "AuctionId" });
            DropIndex("dbo.AuctionFile", new[] { "AuctionId" });
            DropTable("dbo.Stock");
            DropTable("dbo.AuctionHistory");
            DropTable("dbo.Auction");
            DropTable("dbo.AuctionFile");
        }
    }
}
