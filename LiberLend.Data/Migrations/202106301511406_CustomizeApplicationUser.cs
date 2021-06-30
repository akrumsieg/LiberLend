namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomizeApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "FirstName", c => c.String());
            AddColumn("dbo.ApplicationUser", "LastName", c => c.String());
            AddColumn("dbo.ApplicationUser", "StreetAddress", c => c.String());
            AddColumn("dbo.ApplicationUser", "City", c => c.String());
            AddColumn("dbo.ApplicationUser", "State", c => c.String());
            AddColumn("dbo.ApplicationUser", "ZipCode", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "ZipCode");
            DropColumn("dbo.ApplicationUser", "State");
            DropColumn("dbo.ApplicationUser", "City");
            DropColumn("dbo.ApplicationUser", "StreetAddress");
            DropColumn("dbo.ApplicationUser", "LastName");
            DropColumn("dbo.ApplicationUser", "FirstName");
        }
    }
}
