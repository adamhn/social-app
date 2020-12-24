namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Work", c => c.String(maxLength: 55));
            AddColumn("dbo.AspNetUsers", "Study", c => c.String(maxLength: 55));
            AlterColumn("dbo.AspNetUsers", "Firstname", c => c.String(nullable: false, maxLength: 55));
            AlterColumn("dbo.AspNetUsers", "Lastname", c => c.String(nullable: false, maxLength: 55));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Lastname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "Firstname", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.AspNetUsers", "Study");
            DropColumn("dbo.AspNetUsers", "Work");
        }
    }
}
