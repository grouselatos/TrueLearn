namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatnotvm3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatNotifications", "senderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatNotifications", "senderName");
        }
    }
}
