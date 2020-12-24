namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipStatusId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RelationshipStatusId", "dbo.RelationshipStatus");
            DropIndex("dbo.AspNetUsers", new[] { "RelationshipStatusId" });
            AlterColumn("dbo.AspNetUsers", "RelationshipStatusId", c => c.Byte(nullable: false));
            CreateIndex("dbo.AspNetUsers", "RelationshipStatusId");
            AddForeignKey("dbo.AspNetUsers", "RelationshipStatusId", "dbo.RelationshipStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RelationshipStatusId", "dbo.RelationshipStatus");
            DropIndex("dbo.AspNetUsers", new[] { "RelationshipStatusId" });
            AlterColumn("dbo.AspNetUsers", "RelationshipStatusId", c => c.Byte());
            CreateIndex("dbo.AspNetUsers", "RelationshipStatusId");
            AddForeignKey("dbo.AspNetUsers", "RelationshipStatusId", "dbo.RelationshipStatus", "Id");
        }
    }
}
