namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLibrary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Library",
                c => new
                    {
                        LibraryId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LibraryId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Library", "ApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.Library", new[] { "ApplicationUserId" });
            DropTable("dbo.Library");
        }
    }
}
