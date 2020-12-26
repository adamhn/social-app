namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendAndUpdateFlag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestedById = c.Int(nullable: false),
                        RequestTime = c.DateTime(),
                        FriendRequestFlag = c.Int(nullable: false),
                        RequestedBy_Id = c.String(maxLength: 128),
                        RequestedTo_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedTo_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.RequestedBy_Id)
                .Index(t => t.RequestedTo_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "RequestedById", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Friends", new[] { "RequestedTo_Id" });
            DropIndex("dbo.Friends", new[] { "RequestedBy_Id" });
            DropColumn("dbo.AspNetUsers", "RequestedById");
            DropTable("dbo.Friends");
        }
    }
}
