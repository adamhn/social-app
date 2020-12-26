namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriendsModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "RequestedById");
            DropColumn("dbo.Friends", "RequestedById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "RequestedById", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RequestedById", c => c.Int(nullable: false));
        }
    }
}
