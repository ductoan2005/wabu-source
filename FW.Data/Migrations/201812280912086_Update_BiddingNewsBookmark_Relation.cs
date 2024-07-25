namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_BiddingNewsBookmark_Relation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tbl_bidding_news_bookmark", new[] { "BiddingNewsId" });
            AlterColumn("dbo.tbl_bidding_news_bookmark", "BiddingNewsId", c => c.Long(nullable: false));
            CreateIndex("dbo.tbl_bidding_news_bookmark", "BiddingNewsId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_bidding_news_bookmark", new[] { "BiddingNewsId" });
            AlterColumn("dbo.tbl_bidding_news_bookmark", "BiddingNewsId", c => c.Long());
            CreateIndex("dbo.tbl_bidding_news_bookmark", "BiddingNewsId");
        }
    }
}
