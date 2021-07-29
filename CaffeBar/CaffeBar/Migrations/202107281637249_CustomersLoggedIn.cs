namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersLoggedIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "loggedIn", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "loggedIn");
        }
    }
}
