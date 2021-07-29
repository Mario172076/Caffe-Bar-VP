namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductInReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductsInReservation",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        resId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.resId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.resId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsInReservation", "resId", "dbo.Reservations");
            DropForeignKey("dbo.ProductsInReservation", "productId", "dbo.Products");
            DropIndex("dbo.ProductsInReservation", new[] { "resId" });
            DropIndex("dbo.ProductsInReservation", new[] { "productId" });
            DropTable("dbo.ProductsInReservation");
        }
    }
}
