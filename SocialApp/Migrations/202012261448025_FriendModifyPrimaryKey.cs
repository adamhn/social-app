namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendModifyPrimaryKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "FriendId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Friends", "FriendId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Friends");
            DropColumn("dbo.Friends", "FriendId");
            AddPrimaryKey("dbo.Friends", "Id");
        }
    }
}
