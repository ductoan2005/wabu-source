namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyCompanyForCompanyProfile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_company_profile", "CompanyId", c => c.Long(nullable: false));
            CreateIndex("dbo.tbl_company_profile", "CompanyId");
            AddForeignKey("dbo.tbl_company_profile", "CompanyId", "dbo.tbl_company", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_company_profile", "CompanyId", "dbo.tbl_company");
            DropIndex("dbo.tbl_company_profile", new[] { "CompanyId" });
            AlterColumn("dbo.tbl_company_profile", "CompanyId", c => c.Long());
        }
    }
}
