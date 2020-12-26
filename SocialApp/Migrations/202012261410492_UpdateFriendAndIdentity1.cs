namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriendAndIdentity1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Friends", "RequestedById");
            DropColumn("dbo.AspNetUsers", "RequestedById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RequestedById", c => c.Int(nullable: false));
            AddColumn("dbo.Friends", "RequestedById", c => c.Int(nullable: false));
        }
    }
}
