namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionOfferUserRelation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuctionOffer", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AuctionOffer", "UserId");
            AddForeignKey("dbo.AuctionOffer", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuctionOffer", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AuctionOffer", new[] { "UserId" });
            AlterColumn("dbo.AuctionOffer", "UserId", c => c.String());
        }
    }
}
