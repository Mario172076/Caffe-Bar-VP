namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "pay", c => c.Int());
            AlterColumn("dbo.Orders", "orderPrice", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "orderPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "pay", c => c.Int(nullable: false));
        }
    }
}
