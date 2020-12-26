namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriend2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Friends", new[] { "RequestedBy_Id" });
            DropIndex("dbo.Friends", new[] { "RequestedTo_Id" });
            DropColumn("dbo.Friends", "RequestedById");
            DropColumn("dbo.Friends", "RequestedToId");
            RenameColumn(table: "dbo.Friends", name: "RequestedBy_Id", newName: "RequestedById");
            RenameColumn(table: "dbo.Friends", name: "RequestedTo_Id", newName: "RequestedToId");
            AlterColumn("dbo.Friends", "RequestedById", c => c.String(maxLength: 128));
            AlterColumn("dbo.Friends", "RequestedToId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Friends", "RequestedById");
            CreateIndex("dbo.Friends", "RequestedToId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Friends", new[] { "RequestedToId" });
            DropIndex("dbo.Friends", new[] { "RequestedById" });
            AlterColumn("dbo.Friends", "RequestedToId", c => c.Int(nullable: false));
            AlterColumn("dbo.Friends", "RequestedById", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Friends", name: "RequestedToId", newName: "RequestedTo_Id");
            RenameColumn(table: "dbo.Friends", name: "RequestedById", newName: "RequestedBy_Id");
            AddColumn("dbo.Friends", "RequestedToId", c => c.Int(nullable: false));
            AddColumn("dbo.Friends", "RequestedById", c => c.Int(nullable: false));
            CreateIndex("dbo.Friends", "RequestedTo_Id");
            CreateIndex("dbo.Friends", "RequestedBy_Id");
        }
    }
}
