namespace BoardGame1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Membership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.MembershipId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        GamePrice = c.Int(nullable: false),
                        RentalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Rentals", t => t.RentalId, cascadeDelete: true)
                .Index(t => t.RentalId);
            
            CreateTable(
                "dbo.GameMemberships",
                c => new
                    {
                        Game_GameId = c.Int(nullable: false),
                        Membership_MembershipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_GameId, t.Membership_MembershipId })
                .ForeignKey("dbo.Games", t => t.Game_GameId, cascadeDelete: true)
                .ForeignKey("dbo.Memberships", t => t.Membership_MembershipId, cascadeDelete: true)
                .Index(t => t.Game_GameId)
                .Index(t => t.Membership_MembershipId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "RentalId", "dbo.Rentals");
            DropForeignKey("dbo.GameMemberships", "Membership_MembershipId", "dbo.Memberships");
            DropForeignKey("dbo.GameMemberships", "Game_GameId", "dbo.Games");
            DropIndex("dbo.GameMemberships", new[] { "Membership_MembershipId" });
            DropIndex("dbo.GameMemberships", new[] { "Game_GameId" });
            DropIndex("dbo.Games", new[] { "RentalId" });
            DropTable("dbo.GameMemberships");
            DropTable("dbo.Games");
            DropTable("dbo.Memberships");
        }
    }
}
