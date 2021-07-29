namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Int(nullable: false, identity: true),
                        empId = c.Int(nullable: false),
                        custId = c.Int(nullable: false),
                        tableId = c.Int(),
                        status = c.Int(nullable: false),
                        timeToDeliver = c.Int(),
                        orderAddress = c.String(),
                        orderPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.orderId)
                .ForeignKey("dbo.Customers", t => t.custId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.empId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.tableId)
                .Index(t => t.empId)
                .Index(t => t.custId)
                .Index(t => t.tableId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        tableId = c.Int(nullable: false, identity: true),
                        empId = c.Int(nullable: false),
                        numberOfSeats = c.Int(nullable: false),
                        tableAvalaible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tableId)
                .ForeignKey("dbo.Employees", t => t.empId, cascadeDelete: true)
                .Index(t => t.empId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "tableId", "dbo.Tables");
            DropForeignKey("dbo.Tables", "empId", "dbo.Employees");
            DropForeignKey("dbo.Orders", "empId", "dbo.Employees");
            DropForeignKey("dbo.Orders", "custId", "dbo.Customers");
            DropIndex("dbo.Tables", new[] { "empId" });
            DropIndex("dbo.Orders", new[] { "tableId" });
            DropIndex("dbo.Orders", new[] { "custId" });
            DropIndex("dbo.Orders", new[] { "empId" });
            DropTable("dbo.Tables");
            DropTable("dbo.Orders");
        }
    }
}
