namespace Petronas.SampleApp.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SampleApp3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyId" });
            DropPrimaryKey("dbo.AuditClient");
            DropPrimaryKey("mdm.Company");
            DropPrimaryKey("dbo.Navigation");
            DropPrimaryKey("dbo.RefreshToken");
            AddColumn("mdm.Company", "UserId", c => c.Guid());
            AlterColumn("dbo.AuditClient", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("mdm.Company", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Navigation", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.RefreshToken", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.AuditClient", "Id");
            AddPrimaryKey("mdm.Company", "Id");
            AddPrimaryKey("dbo.Navigation", "Id");
            AddPrimaryKey("dbo.RefreshToken", "Id");
            CreateIndex("mdm.Company", "UserId");
            AddForeignKey("mdm.Company", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CompanyId", c => c.Guid());
            DropForeignKey("mdm.Company", "UserId", "dbo.AspNetUsers");
            DropIndex("mdm.Company", new[] { "UserId" });
            DropPrimaryKey("dbo.RefreshToken");
            DropPrimaryKey("dbo.Navigation");
            DropPrimaryKey("mdm.Company");
            DropPrimaryKey("dbo.AuditClient");
            AlterColumn("dbo.RefreshToken", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Navigation", "Id", c => c.Guid(nullable: false));
            AlterColumn("mdm.Company", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.AuditClient", "Id", c => c.Guid(nullable: false));
            DropColumn("mdm.Company", "UserId");
            AddPrimaryKey("dbo.RefreshToken", "Id");
            AddPrimaryKey("dbo.Navigation", "Id");
            AddPrimaryKey("mdm.Company", "Id");
            AddPrimaryKey("dbo.AuditClient", "Id");
            CreateIndex("dbo.AspNetUsers", "CompanyId");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "mdm.Company", "Id");
        }
    }
}
