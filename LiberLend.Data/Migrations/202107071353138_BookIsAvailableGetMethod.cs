namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookIsAvailableGetMethod : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Book", "IsAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "IsAvailable", c => c.Boolean(nullable: false));
        }
    }
}
