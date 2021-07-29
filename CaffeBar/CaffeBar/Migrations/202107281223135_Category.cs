namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        catId = c.Int(nullable: false, identity: true),
                        catName = c.String(),
                    })
                .PrimaryKey(t => t.catId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
