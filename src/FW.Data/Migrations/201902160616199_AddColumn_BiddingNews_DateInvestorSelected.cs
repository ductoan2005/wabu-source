namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_BiddingNews_DateInvestorSelected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news", "DateInvestorSelected", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_news", "DateInvestorSelected");
        }
    }
}
