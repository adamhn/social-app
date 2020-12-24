namespace SocialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRelationshipStatus : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (1, '-')");
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (2, 'Single')");
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (3, 'In a relationship')");
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (4, 'Engaged')");
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (5, 'Married')");
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (6, 'Separated')");
            Sql("INSERT INTO RelationshipStatus (Id, Status) VALUES (7, 'Divorced')");
        }
        
        public override void Down()
        {
        }
    }
}
