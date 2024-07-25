namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedDateTimeToDateTimeNullable_BiddingNews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_bidding_news", "DurationContract", c => c.DateTime());
            AlterColumn("dbo.tbl_bidding_news", "BidStartDate", c => c.DateTime());
            AlterColumn("dbo.tbl_bidding_news", "BidCloseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_bidding_news", "BidCloseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tbl_bidding_news", "BidStartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tbl_bidding_news", "DurationContract", c => c.DateTime(nullable: false));
        }
    }
}
