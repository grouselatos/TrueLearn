namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branchmigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "birth_date");
            DropColumn("dbo.AspNetUsers", "country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "country", c => c.String());
            AddColumn("dbo.AspNetUsers", "birth_date", c => c.DateTime());
        }
    }
}
