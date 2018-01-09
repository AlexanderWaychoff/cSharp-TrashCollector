namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "AccountID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "AccountID", c => c.Int(nullable: false));
        }
    }
}
