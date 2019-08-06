namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatnotvm2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ChatNotifications", "senderName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatNotifications", "senderName", c => c.String());
        }
    }
}
