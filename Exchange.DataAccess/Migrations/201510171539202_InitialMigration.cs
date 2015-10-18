namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auction", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auction", "UserId");
        }
    }
}
