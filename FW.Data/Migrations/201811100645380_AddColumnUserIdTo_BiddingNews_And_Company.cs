namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnUserIdTo_BiddingNews_And_Company : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_construction", "UserId", "dbo.tbl_users");
            AddColumn("dbo.tbl_bidding_news", "UserId", c => c.Long());
            CreateIndex("dbo.tbl_bidding_news", "UserId");
            CreateIndex("dbo.tbl_company", "UserId");
            AddForeignKey("dbo.tbl_bidding_news", "UserId", "dbo.tbl_users", "Id");
            AddForeignKey("dbo.tbl_company", "UserId", "dbo.tbl_users", "Id");
            AddForeignKey("dbo.tbl_construction", "UserId", "dbo.tbl_users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_construction", "UserId", "dbo.tbl_users");
            DropForeignKey("dbo.tbl_company", "UserId", "dbo.tbl_users");
            DropForeignKey("dbo.tbl_bidding_news", "UserId", "dbo.tbl_users");
            DropIndex("dbo.tbl_company", new[] { "UserId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "UserId" });
            DropColumn("dbo.tbl_bidding_news", "UserId");
            AddForeignKey("dbo.tbl_construction", "UserId", "dbo.tbl_users", "Id", cascadeDelete: true);
        }
    }
}
