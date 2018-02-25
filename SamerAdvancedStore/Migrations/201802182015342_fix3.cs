namespace SamerAdvancedStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Transactions", "InvintoryId", "dbo.Invintoreys");
            AddForeignKey("dbo.Transactions", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "InvintoryId", "dbo.Invintoreys", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "InvintoryId", "dbo.Invintoreys");
            DropForeignKey("dbo.Transactions", "OrderId", "dbo.Orders");
            AddForeignKey("dbo.Transactions", "InvintoryId", "dbo.Invintoreys", "Id");
            AddForeignKey("dbo.Transactions", "OrderId", "dbo.Orders", "Id");
        }
    }
}
