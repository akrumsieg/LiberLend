namespace LiberLend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeIsbnNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "ISBN", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "ISBN", c => c.Int(nullable: false));
        }
    }
}
