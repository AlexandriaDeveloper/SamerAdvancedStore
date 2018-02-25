namespace SamerAdvancedStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
          //  AlterColumn("dbo.Transactions", "Sold", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
        //    AlterColumn("dbo.Transactions", "Sold", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
