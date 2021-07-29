namespace CaffeBar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        empid = c.Int(nullable: false, identity: true),
                        empName = c.String(),
                        empSurname = c.String(),
                        empTelephone = c.String(),
                        empUsername = c.String(),
                        empPassword = c.String(),
                        pay = c.Int(nullable: false),
                        payPerHour = c.Int(nullable: false),
                        workHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.empid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
