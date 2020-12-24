namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelationshipStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "RelationshipStatusId", c => c.Byte());
            CreateIndex("dbo.AspNetUsers", "RelationshipStatusId");
            AddForeignKey("dbo.AspNetUsers", "RelationshipStatusId", "dbo.RelationshipStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RelationshipStatusId", "dbo.RelationshipStatus");
            DropIndex("dbo.AspNetUsers", new[] { "RelationshipStatusId" });
            DropColumn("dbo.AspNetUsers", "RelationshipStatusId");
            DropTable("dbo.RelationshipStatus");
        }
    }
}
