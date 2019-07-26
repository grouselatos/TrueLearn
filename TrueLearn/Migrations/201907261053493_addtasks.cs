namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        task = c.Int(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTasks", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseTasks", new[] { "CourseId" });
            DropTable("dbo.CourseTasks");
        }
    }
}
