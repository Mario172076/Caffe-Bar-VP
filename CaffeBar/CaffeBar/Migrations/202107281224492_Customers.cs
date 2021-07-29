namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        custId = c.Int(nullable: false, identity: true),
                        custName = c.String(),
                        custSurname = c.String(),
                        custTelephone = c.String(),
                        custUsername = c.String(),
                        custPassword = c.String(),
                        address = c.String(),
                        age = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.custId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
