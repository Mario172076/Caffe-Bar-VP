namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsInOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductsInOrder",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        orderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.orderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsInOrder", "productId", "dbo.Products");
            DropForeignKey("dbo.ProductsInOrder", "orderId", "dbo.Orders");
            DropIndex("dbo.ProductsInOrder", new[] { "orderId" });
            DropIndex("dbo.ProductsInOrder", new[] { "productId" });
            DropTable("dbo.ProductsInOrder");
        }
    }
}
