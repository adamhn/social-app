﻿namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeduserprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Picture");
        }
    }
}
