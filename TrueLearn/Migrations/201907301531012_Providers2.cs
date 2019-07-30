namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Providers2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "provider", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "provider", c => c.String());
        }
    }
}
