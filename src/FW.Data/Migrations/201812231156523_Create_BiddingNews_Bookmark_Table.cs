namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_BiddingNews_Bookmark_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_bidding_news_bookmark",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BiddingNewsId = c.Long(),
                        BookmarkDate = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_news", t => t.BiddingNewsId)
                .Index(t => t.BiddingNewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_bidding_news_bookmark", "BiddingNewsId", "dbo.tbl_bidding_news");
            DropIndex("dbo.tbl_bidding_news_bookmark", new[] { "BiddingNewsId" });
            DropTable("dbo.tbl_bidding_news_bookmark");
        }
    }
}
