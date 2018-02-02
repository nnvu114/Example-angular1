namespace Petronas.SampleApp.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleApp4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("mdm.Company", "UserId", "dbo.AspNetUsers");
            DropIndex("mdm.Company", new[] { "UserId" });
            AddColumn("dbo.AspNetUsers", "CompanyId", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "CompanyId");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company", "Id");
            DropColumn("mdm.Company", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("mdm.Company", "UserId", c => c.Guid());
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyId" });
            DropColumn("dbo.AspNetUsers", "CompanyId");
            CreateIndex("mdm.Company", "UserId");
            AddForeignKey("mdm.Company", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
