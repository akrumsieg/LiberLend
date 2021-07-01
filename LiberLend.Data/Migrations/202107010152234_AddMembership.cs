namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Membership",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        LibraryId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        IsAuthorized = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .ForeignKey("dbo.Library", t => t.LibraryId, cascadeDelete: true)
                .Index(t => t.LibraryId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Membership", "LibraryId", "dbo.Library");
            DropForeignKey("dbo.Membership", "ApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.Membership", new[] { "ApplicationUserId" });
            DropIndex("dbo.Membership", new[] { "LibraryId" });
            DropTable("dbo.Membership");
        }
    }
}
