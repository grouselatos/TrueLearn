namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "triggered", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "triggered");
        }
    }
}
