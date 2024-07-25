namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyProfileToBiddingDetail : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.tbl_bidding_detail", "CompanyProfileId");
            AddForeignKey("dbo.tbl_bidding_detail", "CompanyProfileId", "dbo.tbl_company_profile", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_bidding_detail", "CompanyProfileId", "dbo.tbl_company_profile");
            DropIndex("dbo.tbl_bidding_detail", new[] { "CompanyProfileId" });
        }
    }
}
