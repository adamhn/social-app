namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriend : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Friends");
            DropColumn("dbo.Friends", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Friends", "Id");
        }
    }
}
