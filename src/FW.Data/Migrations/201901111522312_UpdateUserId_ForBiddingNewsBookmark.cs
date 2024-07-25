namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserId_ForBiddingNewsBookmark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news_bookmark", "UserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_news_bookmark", "UserId");
        }
    }
}
