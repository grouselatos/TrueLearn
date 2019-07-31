namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withUI : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CertificatePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        first_name = c.String(),
                        last_name = c.String(),
                        birth_date = c.DateTime(nullable: false),
                        country = c.String(),
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
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        senderId = c.String(maxLength: 128),
                        receiverId = c.String(maxLength: 128),
                        status = c.Int(nullable: false),
                        triggered = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.receiverId)
                .ForeignKey("dbo.AspNetUsers", t => t.senderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.senderId)
                .Index(t => t.receiverId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        sender = c.String(),
                        receiver = c.String(),
                        sent = c.DateTime(nullable: false),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.ChatNotifications",
                c => new
                    {
                        ChatId = c.Int(nullable: false),
                        sender = c.String(),
                        receiver = c.String(),
                        triggered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.Messages", t => t.ChatId)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.FriendNotifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sender = c.String(),
                        receiver = c.String(),
                        triggered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GlobalChats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        sent = c.DateTime(nullable: false),
                        message = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                "dbo.TodoTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        title = c.String(),
                        description = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        headline = c.String(),
                        description = c.String(),
                        image = c.Binary(),
                        provider = c.Int(nullable: false),
                        url = c.String(),
                        title = c.String(),
                        category = c.String(),
                        status = c.Int(nullable: false),
                        tracked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
                "dbo.FriendNotificationFriends",
                c => new
                    {
                        FriendNotification_id = c.Int(nullable: false),
                        Friend_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FriendNotification_id, t.Friend_Id })
                .ForeignKey("dbo.FriendNotifications", t => t.FriendNotification_id, cascadeDelete: true)
                .ForeignKey("dbo.Friends", t => t.Friend_Id, cascadeDelete: true)
                .Index(t => t.FriendNotification_id)
                .Index(t => t.Friend_Id);
            
            CreateTable(
                "dbo.CourseTodoTasks",
                c => new
                    {
                        Course_id = c.Int(nullable: false),
                        TodoTask_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_id, t.TodoTask_Id })
                .ForeignKey("dbo.Courses", t => t.Course_id, cascadeDelete: true)
                .ForeignKey("dbo.TodoTasks", t => t.TodoTask_Id, cascadeDelete: true)
                .Index(t => t.Course_id)
                .Index(t => t.TodoTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Certificates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseTodoTasks", "TodoTask_Id", "dbo.TodoTasks");
            DropForeignKey("dbo.CourseTodoTasks", "Course_id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TodoTasks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GlobalChats", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "senderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "receiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendNotificationFriends", "Friend_Id", "dbo.Friends");
            DropForeignKey("dbo.FriendNotificationFriends", "FriendNotification_id", "dbo.FriendNotifications");
            DropForeignKey("dbo.ChatNotifications", "ChatId", "dbo.Messages");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Chats", "Id", "dbo.Friends");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseTodoTasks", new[] { "TodoTask_Id" });
            DropIndex("dbo.CourseTodoTasks", new[] { "Course_id" });
            DropIndex("dbo.FriendNotificationFriends", new[] { "Friend_Id" });
            DropIndex("dbo.FriendNotificationFriends", new[] { "FriendNotification_id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Courses", new[] { "UserId" });
            DropIndex("dbo.TodoTasks", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.GlobalChats", new[] { "UserId" });
            DropIndex("dbo.ChatNotifications", new[] { "ChatId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Chats", new[] { "Id" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Friends", new[] { "receiverId" });
            DropIndex("dbo.Friends", new[] { "senderId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Certificates", new[] { "UserId" });
            DropTable("dbo.CourseTodoTasks");
            DropTable("dbo.FriendNotificationFriends");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Courses");
            DropTable("dbo.TodoTasks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.GlobalChats");
            DropTable("dbo.FriendNotifications");
            DropTable("dbo.ChatNotifications");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
            DropTable("dbo.Friends");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Certificates");
        }
    }
}
