namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriend1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "RequestedById", c => c.Int(nullable: false));
            AddColumn("dbo.Friends", "RequestedToId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "RequestedToId");
            DropColumn("dbo.Friends", "RequestedById");
        }
    }
}
