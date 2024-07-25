namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyToBiddingDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_bidding_detail", "BiddingNewsId", c => c.Long(nullable: false));
            CreateIndex("dbo.tbl_bidding_detail", "BiddingNewsId");
            AddForeignKey("dbo.tbl_bidding_detail", "BiddingNewsId", "dbo.tbl_bidding_news", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_bidding_detail", "BiddingNewsId", "dbo.tbl_bidding_news");
            DropIndex("dbo.tbl_bidding_detail", new[] { "BiddingNewsId" });
            AlterColumn("dbo.tbl_bidding_detail", "BiddingNewsId", c => c.Long());
        }
    }
}
