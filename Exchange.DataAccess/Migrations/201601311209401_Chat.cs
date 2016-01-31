namespace Exchange.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipientId = c.String(maxLength: 128),
                        SenderId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.SenderId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationId = c.Int(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversation", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ConversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversation", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversation", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Message", "ConversationId", "dbo.Conversation");
            DropIndex("dbo.Message", new[] { "ConversationId" });
            DropIndex("dbo.Conversation", new[] { "SenderId" });
            DropIndex("dbo.Conversation", new[] { "RecipientId" });
            DropTable("dbo.Message");
            DropTable("dbo.Conversation");
        }
    }
}
