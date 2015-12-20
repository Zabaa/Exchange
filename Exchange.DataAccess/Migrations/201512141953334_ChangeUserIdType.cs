namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIdType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuctionHistory", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuctionHistory", "UserId", c => c.Int(nullable: false));
        }
    }
}
