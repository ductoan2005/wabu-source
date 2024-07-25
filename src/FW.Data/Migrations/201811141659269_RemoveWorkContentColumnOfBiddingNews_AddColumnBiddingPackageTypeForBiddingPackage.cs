namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveWorkContentColumnOfBiddingNews_AddColumnBiddingPackageTypeForBiddingPackage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_bidding_news", "BiddingPackageOtherId", "dbo.tbl_bidding_package_other");
            DropForeignKey("dbo.tbl_bidding_news", "WorkContentId", "dbo.tbl_work_content");
            DropForeignKey("dbo.tbl_bidding_news", "WorkContentOtherId", "dbo.tbl_work_content_other");
            DropIndex("dbo.tbl_bidding_news", new[] { "BiddingPackageOtherId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "WorkContentId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "WorkContentOtherId" });
            DropIndex("dbo.tbl_work_content_other", new[] { "BiddingPackageId" });
            DropIndex("dbo.tbl_work_content", new[] { "BiddingPackageId" });
            AddColumn("dbo.tbl_bidding_package", "BiddingPackageType", c => c.Byte(nullable: false));
            AlterColumn("dbo.tbl_work_content_other", "BiddingPackageId", c => c.Long());
            AlterColumn("dbo.tbl_work_content", "BiddingPackageId", c => c.Long());
            CreateIndex("dbo.tbl_work_content_other", "BiddingPackageId");
            CreateIndex("dbo.tbl_work_content", "BiddingPackageId");
            DropColumn("dbo.tbl_bidding_news", "BiddingPackageOtherId");
            DropColumn("dbo.tbl_bidding_news", "WorkContentId");
            DropColumn("dbo.tbl_bidding_news", "WorkContentOtherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_bidding_news", "WorkContentOtherId", c => c.Long());
            AddColumn("dbo.tbl_bidding_news", "WorkContentId", c => c.Long());
            AddColumn("dbo.tbl_bidding_news", "BiddingPackageOtherId", c => c.Long());
            DropIndex("dbo.tbl_work_content", new[] { "BiddingPackageId" });
            DropIndex("dbo.tbl_work_content_other", new[] { "BiddingPackageId" });
            AlterColumn("dbo.tbl_work_content", "BiddingPackageId", c => c.Long(nullable: false));
            AlterColumn("dbo.tbl_work_content_other", "BiddingPackageId", c => c.Long(nullable: false));
            DropColumn("dbo.tbl_bidding_package", "BiddingPackageType");
            CreateIndex("dbo.tbl_work_content", "BiddingPackageId");
            CreateIndex("dbo.tbl_work_content_other", "BiddingPackageId");
            CreateIndex("dbo.tbl_bidding_news", "WorkContentOtherId");
            CreateIndex("dbo.tbl_bidding_news", "WorkContentId");
            CreateIndex("dbo.tbl_bidding_news", "BiddingPackageOtherId");
            AddForeignKey("dbo.tbl_bidding_news", "WorkContentOtherId", "dbo.tbl_work_content_other", "Id");
            AddForeignKey("dbo.tbl_bidding_news", "WorkContentId", "dbo.tbl_work_content", "Id");
            AddForeignKey("dbo.tbl_bidding_news", "BiddingPackageOtherId", "dbo.tbl_bidding_package_other", "Id");
        }
    }
}
