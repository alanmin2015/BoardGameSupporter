namespace BoardGame1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rental : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        RentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RentalId);
            
         
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        GameInventory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            DropTable("dbo.Rentals");
        }
    }
}
