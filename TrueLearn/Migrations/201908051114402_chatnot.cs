namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatnot : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatNotifications", "ChatId", "dbo.Messages");
            DropPrimaryKey("dbo.ChatNotifications");
            AddColumn("dbo.Messages", "ChatNotification_Id", c => c.Int());
            AddColumn("dbo.ChatNotifications", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ChatNotifications", "MessageId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ChatNotifications", "Id");
            CreateIndex("dbo.Messages", "ChatNotification_Id");
            AddForeignKey("dbo.ChatNotifications", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "ChatNotification_Id", "dbo.ChatNotifications", "Id");
            DropColumn("dbo.ChatNotifications", "sender");
            DropColumn("dbo.ChatNotifications", "triggered");
            DropTable("dbo.SuggestionDummies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SuggestionDummies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.ChatNotifications", "triggered", c => c.DateTime(nullable: false));
            AddColumn("dbo.ChatNotifications", "sender", c => c.String());
            DropForeignKey("dbo.Messages", "ChatNotification_Id", "dbo.ChatNotifications");
            DropForeignKey("dbo.ChatNotifications", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "ChatNotification_Id" });
            DropPrimaryKey("dbo.ChatNotifications");
            DropColumn("dbo.ChatNotifications", "MessageId");
            DropColumn("dbo.ChatNotifications", "Id");
            DropColumn("dbo.Messages", "ChatNotification_Id");
            AddPrimaryKey("dbo.ChatNotifications", "ChatId");
            AddForeignKey("dbo.ChatNotifications", "ChatId", "dbo.Messages", "Id");
        }
    }
}
