namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilesToBiddingDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_bidding_detail_file", "BiddingDetailId", "dbo.tbl_bidding_detail");
            DropIndex("dbo.tbl_bidding_detail_file", new[] { "BiddingDetailId" });
            AddColumn("dbo.tbl_bidding_detail_file", "BiddingDetail_Id", c => c.Long());
            CreateIndex("dbo.tbl_bidding_detail_file", "BiddingDetail_Id");
            AddForeignKey("dbo.tbl_bidding_detail_file", "BiddingDetail_Id", "dbo.tbl_bidding_detail", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_bidding_detail_file", "BiddingDetail_Id", "dbo.tbl_bidding_detail");
            DropIndex("dbo.tbl_bidding_detail_file", new[] { "BiddingDetail_Id" });
            DropColumn("dbo.tbl_bidding_detail_file", "BiddingDetail_Id");
            CreateIndex("dbo.tbl_bidding_detail_file", "BiddingDetailId");
            AddForeignKey("dbo.tbl_bidding_detail_file", "BiddingDetailId", "dbo.tbl_bidding_detail", "Id", cascadeDelete: true);
        }
    }
}
