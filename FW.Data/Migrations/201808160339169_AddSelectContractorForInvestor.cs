namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectContractorForInvestor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_detail", "InvestorSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_detail", "InvestorSelected");
        }
    }
}
