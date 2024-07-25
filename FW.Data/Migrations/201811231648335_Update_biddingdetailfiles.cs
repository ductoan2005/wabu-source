namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_biddingdetailfiles : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_technical_other", newName: "tbl_bidding_news_technical_other");
            DropIndex("dbo.tbl_bidding_detail_file", new[] { "BiddingDetail_Id" });
            AlterColumn("dbo.tbl_bidding_detail_file", "BiddingDetailId", c => c.Long(nullable: false));
            CreateIndex("dbo.tbl_bidding_detail_file", "BiddingDetailId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_bidding_detail_file", new[] { "BiddingDetailId" });
            AlterColumn("dbo.tbl_bidding_detail_file", "BiddingDetailId", c => c.Long());
            CreateIndex("dbo.tbl_bidding_detail_file", "BiddingDetail_Id");
            RenameTable(name: "dbo.tbl_bidding_news_technical_other", newName: "tbl_technical_other");
        }
    }
}
