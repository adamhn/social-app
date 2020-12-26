namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMigraitonKeysAndIdentityModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedToId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "RequestedById" });
            DropIndex("dbo.Friends", new[] { "RequestedToId" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Friends", "RequestedById", c => c.String());
            AlterColumn("dbo.Friends", "RequestedToId", c => c.String());
            DropColumn("dbo.Friends", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Friends", "RequestedToId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Friends", "RequestedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.Friends", "ApplicationUser_Id");
            CreateIndex("dbo.Friends", "RequestedToId");
            CreateIndex("dbo.Friends", "RequestedById");
            AddForeignKey("dbo.Friends", "RequestedToId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "RequestedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
