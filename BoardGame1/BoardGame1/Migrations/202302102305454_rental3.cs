namespace BoardGame1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rental3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rentals", "RentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rentals", "RentDate", c => c.String());
        }
    }
}
