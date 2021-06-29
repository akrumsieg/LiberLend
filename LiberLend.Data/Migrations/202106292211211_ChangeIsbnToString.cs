namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIsbnToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "ISBN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "ISBN", c => c.Int());
        }
    }
}
