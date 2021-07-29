namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        resId = c.Int(nullable: false, identity: true),
                        custId = c.Int(nullable: false),
                        tableId = c.Int(nullable: false),
                        dateRes = c.DateTime(nullable: false),
                        numPeople = c.Int(nullable: false),
                        MinPriceRes = c.Int(),
                        priceRes = c.Int(),
                    })
                .PrimaryKey(t => t.resId)
                .ForeignKey("dbo.Customers", t => t.custId, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.tableId, cascadeDelete: true)
                .Index(t => t.custId)
                .Index(t => t.tableId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "tableId", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "custId", "dbo.Customers");
            DropIndex("dbo.Reservations", new[] { "tableId" });
            DropIndex("dbo.Reservations", new[] { "custId" });
            DropTable("dbo.Reservations");
        }
    }
}
