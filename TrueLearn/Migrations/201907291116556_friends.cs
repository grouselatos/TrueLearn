namespace TrueLearn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friends : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Friends", name: "receiver_Id", newName: "receiverId");
            RenameColumn(table: "dbo.Friends", name: "sender_Id", newName: "senderId");
            RenameIndex(table: "dbo.Friends", name: "IX_sender_Id", newName: "IX_senderId");
            RenameIndex(table: "dbo.Friends", name: "IX_receiver_Id", newName: "IX_receiverId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Friends", name: "IX_receiverId", newName: "IX_receiver_Id");
            RenameIndex(table: "dbo.Friends", name: "IX_senderId", newName: "IX_sender_Id");
            RenameColumn(table: "dbo.Friends", name: "senderId", newName: "sender_Id");
            RenameColumn(table: "dbo.Friends", name: "receiverId", newName: "receiver_Id");
        }
    }
}
