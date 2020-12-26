namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendModifyPrimaryKey1 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Friends");
            AddColumn("dbo.Friends", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Friends", "Id");
            //DropColumn("dbo.Friends", "FriendId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Friends", "FriendId", c => c.Int(nullable: false, identity: true));
            //DropPrimaryKey("dbo.Friends");
            //DropColumn("dbo.Friends", "Id");
            //AddPrimaryKey("dbo.Friends", "FriendId");
        }
    }
}
