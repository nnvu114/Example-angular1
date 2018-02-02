namespace Petronas.SampleApp.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleApp1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("mdm.Company", "ContactPersonName", c => c.String());
            AddColumn("mdm.Company", "CompanyNumber", c => c.String());
            AddColumn("mdm.Company", "IsPetronasLicensedCompany", c => c.Boolean(nullable: false));
            AddColumn("mdm.Company", "RegistrationSWECCode", c => c.String());
            DropColumn("mdm.Company", "Description");
        }
        
        public override void Down()
        {
            AddColumn("mdm.Company", "Description", c => c.String());
            DropColumn("mdm.Company", "RegistrationSWECCode");
            DropColumn("mdm.Company", "IsPetronasLicensedCompany");
            DropColumn("mdm.Company", "CompanyNumber");
            DropColumn("mdm.Company", "ContactPersonName");
        }
    }
}
