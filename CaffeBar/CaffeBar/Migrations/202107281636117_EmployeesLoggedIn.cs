namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeesLoggedIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "loggedIn", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "loggedIn");
        }
    }
}
