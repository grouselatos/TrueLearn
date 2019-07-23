namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "UserId", c => c.String());
            AddColumn("dbo.Courses", "provider", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "provider");
            DropColumn("dbo.Courses", "UserId");
        }
    }
}
