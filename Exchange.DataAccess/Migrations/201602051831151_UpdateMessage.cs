namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "IsSender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "IsSender");
        }
    }
}
