namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        StartTime = c.DateTimeOffset(nullable: false, precision: 7),
                        EndTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "BookId", "dbo.Book");
            DropForeignKey("dbo.Reservation", "ApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.Reservation", new[] { "ApplicationUserId" });
            DropIndex("dbo.Reservation", new[] { "BookId" });
            DropTable("dbo.Reservation");
        }
    }
}
