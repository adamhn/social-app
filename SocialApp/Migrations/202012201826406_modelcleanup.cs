namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelcleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.AspNetUsers", "Firstname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "Lastname", c => c.String(nullable: false, maxLength: 255));
            DropTable("dbo.Friends");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FriendId);
            
            AlterColumn("dbo.AspNetUsers", "Lastname", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            CreateIndex("dbo.Friends", "ApplicationUser_Id");
            AddForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
