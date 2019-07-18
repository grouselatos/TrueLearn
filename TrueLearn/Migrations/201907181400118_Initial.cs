namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        friends_id = c.Int(nullable: false),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        sender_id = c.Int(nullable: false),
                        transcript = c.String(),
                    })
                .PrimaryKey(t => t.friends_id)
                .ForeignKey("dbo.Friends", t => t.friends_id)
                .Index(t => t.friends_id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        status = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        requested_from_id_Id = c.String(maxLength: 128),
                        requested_to_id_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.requested_from_id_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.requested_to_id_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.requested_from_id_Id)
                .Index(t => t.requested_to_id_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        first_name = c.String(),
                        last_name = c.String(),
                        birth_date = c.DateTime(nullable: false),
                        country = c.String(),
                        premium = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        category = c.String(),
                        started = c.Boolean(nullable: false),
                        completed = c.Boolean(nullable: false),
                        tracked = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Premium_User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.premium_users", t => t.Premium_User_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Premium_User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.premium_users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.tracked_courses",
                c => new
                    {
                        id = c.Int(nullable: false),
                        task = c.Int(nullable: false),
                        description = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courses", t => t.id)
                .Index(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tracked_courses", "id", "dbo.Courses");
            DropForeignKey("dbo.premium_users", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Chats", "friends_id", "dbo.Friends");
            DropForeignKey("dbo.Friends", "requested_to_id_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "requested_from_id_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Premium_User_Id", "dbo.premium_users");
            DropForeignKey("dbo.Courses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.tracked_courses", new[] { "id" });
            DropIndex("dbo.premium_users", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Courses", new[] { "Premium_User_Id" });
            DropIndex("dbo.Courses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Friends", new[] { "requested_to_id_Id" });
            DropIndex("dbo.Friends", new[] { "requested_from_id_Id" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Chats", new[] { "friends_id" });
            DropTable("dbo.tracked_courses");
            DropTable("dbo.premium_users");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Courses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Friends");
            DropTable("dbo.Chats");
        }
    }
}
