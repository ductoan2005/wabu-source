namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnForBiddingNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news", "NewsApprovalDate", c => c.DateTime());
            AddColumn("dbo.tbl_bidding_news", "NumberOfDaysImplement", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_news", "NumberOfDaysImplement");
            DropColumn("dbo.tbl_bidding_news", "NewsApprovalDate");
        }
    }
}
