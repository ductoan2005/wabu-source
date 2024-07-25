namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BiddingNews_Budget_Col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news", "BudgetImplementation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_company", "OrganizationalChartName", c => c.String());
            AddColumn("dbo.tbl_company", "OrganizationalChartPath", c => c.String());
            DropColumn("dbo.tbl_company", "OrganizationalChart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_company", "OrganizationalChart", c => c.String());
            DropColumn("dbo.tbl_company", "OrganizationalChartPath");
            DropColumn("dbo.tbl_company", "OrganizationalChartName");
            DropColumn("dbo.tbl_bidding_news", "BudgetImplementation");
        }
    }
}
