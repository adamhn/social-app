namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FriendId_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "FriendId_Id");
            AddForeignKey("dbo.AspNetUsers", "FriendId_Id", "dbo.Friends", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "FriendId_Id", "dbo.Friends");
            DropIndex("dbo.AspNetUsers", new[] { "FriendId_Id" });
            DropColumn("dbo.AspNetUsers", "FriendId_Id");
        }
    }
}
