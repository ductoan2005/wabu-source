namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyStatusBiddingNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news", "StatusBiddingNews", c => c.Byte(nullable: false));
            DropColumn("dbo.tbl_bidding_news", "StatusInvestor");
            DropColumn("dbo.tbl_bidding_news", "StatusContractor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_bidding_news", "StatusContractor", c => c.Byte(nullable: false));
            AddColumn("dbo.tbl_bidding_news", "StatusInvestor", c => c.Byte(nullable: false));
            DropColumn("dbo.tbl_bidding_news", "StatusBiddingNews");
        }
    }
}
