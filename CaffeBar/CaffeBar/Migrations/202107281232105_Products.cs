namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        proId = c.Int(nullable: false, identity: true),
                        catId = c.Int(nullable: false),
                        proPrice = c.Int(nullable: false),
                        proName = c.String(),
                        timeOfServing = c.Int(),
                        ageRestrictions = c.Int(nullable: false),
                        proDescription = c.String(),
                        proQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.proId)
                .ForeignKey("dbo.Categories", t => t.catId, cascadeDelete: true)
                .Index(t => t.catId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "catId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "catId" });
            DropTable("dbo.Products");
        }
    }
}
