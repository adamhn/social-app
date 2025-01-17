﻿namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePostWithFullname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostedByFullname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostedByFullname");
        }
    }
}
