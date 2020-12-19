namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterfriends_remove_field : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Friends", "UserReferenceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "UserReferenceId", c => c.Int(nullable: false));
        }
    }
}
