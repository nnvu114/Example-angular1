namespace Petronas.SampleApp.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleApp2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company");
            DropPrimaryKey("dbo.AuditClient");
            DropPrimaryKey("mdm.Company");
            DropPrimaryKey("dbo.Navigation");
            DropPrimaryKey("dbo.RefreshToken");
            AlterColumn("dbo.AuditClient", "Id", c => c.Guid(nullable: false));
            AlterColumn("mdm.Company", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Navigation", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.RefreshToken", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.AuditClient", "Id");
            AddPrimaryKey("mdm.Company", "Id");
            AddPrimaryKey("dbo.Navigation", "Id");
            AddPrimaryKey("dbo.RefreshToken", "Id");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company");
            DropPrimaryKey("dbo.RefreshToken");
            DropPrimaryKey("dbo.Navigation");
            DropPrimaryKey("mdm.Company");
            DropPrimaryKey("dbo.AuditClient");
            AlterColumn("dbo.RefreshToken", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Navigation", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("mdm.Company", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.AuditClient", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.RefreshToken", "Id");
            AddPrimaryKey("dbo.Navigation", "Id");
            AddPrimaryKey("mdm.Company", "Id");
            AddPrimaryKey("dbo.AuditClient", "Id");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company", "Id");
        }
    }
}
