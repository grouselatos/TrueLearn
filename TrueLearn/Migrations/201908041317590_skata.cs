namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skata : DbMigration
    {
        public override void Up()
        {
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
            
        }
    }
}
