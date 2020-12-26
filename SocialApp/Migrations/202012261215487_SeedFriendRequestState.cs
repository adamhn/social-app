namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedFriendRequestState : DbMigration
    {
        public override void Up()
        {
            //Sql("INSERT INTO FriendRequestState (Id, Name) VALUES (1, 'None')");
            //Sql("INSERT INTO FriendRequestState (Id, Name) VALUES (1, 'Approved')");
            //Sql("INSERT INTO FriendRequestState (Id, Name) VALUES (1, 'Rejected')");
            //Sql("INSERT INTO FriendRequestState (Id, Name) VALUES (1, 'Awaiting')");
        }

        public override void Down()
        {
        }
    }
}
