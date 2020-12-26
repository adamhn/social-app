namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriendAndIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "RequestedById", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RequestedById", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RequestedById");
            DropColumn("dbo.Friends", "RequestedById");
        }
    }
}
