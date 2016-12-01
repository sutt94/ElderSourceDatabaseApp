namespace ElderSourceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyModel",
                c => new
                    {
                        CompanyModelID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CompanyType = c.String(),
                        Phone = c.String(),
                        HasSymbol = c.Boolean(nullable: false),
                        EmployeesTrained = c.Boolean(nullable: false),
                        HasPolicies = c.Boolean(nullable: false),
                        HasDeclaration = c.Boolean(nullable: false),
                        InArrears = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyModelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompanyModel");
        }
    }
}
