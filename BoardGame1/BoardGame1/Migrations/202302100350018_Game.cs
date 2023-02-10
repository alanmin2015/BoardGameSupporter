namespace BoardGame1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GameMemberships", newName: "MembershipGames");
            DropPrimaryKey("dbo.MembershipGames");
            AddPrimaryKey("dbo.MembershipGames", new[] { "Membership_MembershipId", "Game_GameId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MembershipGames");
            AddPrimaryKey("dbo.MembershipGames", new[] { "Game_GameId", "Membership_MembershipId" });
            RenameTable(name: "dbo.MembershipGames", newName: "GameMemberships");
        }
    }
}
