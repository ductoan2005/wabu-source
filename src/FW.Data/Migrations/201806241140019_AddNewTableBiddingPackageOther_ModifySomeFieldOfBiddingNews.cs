namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableBiddingPackageOther_ModifySomeFieldOfBiddingNews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_bidding_news", "BiddingPackageId", "dbo.tbl_bidding_package");
            DropIndex("dbo.tbl_bidding_news", new[] { "BiddingPackageId" });
            CreateTable(
                "dbo.tbl_bidding_package_other",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BiddingPackageOtherName = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tbl_bidding_news", "BiddingPackageOtherId", c => c.Long());
            AddColumn("dbo.tbl_bidding_news", "YearOfTurnoverAbilityFinance", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_bidding_news", "YearFinanceSituationAbilityFinance", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_bidding_news", "BiddingPackageId", c => c.Long());
            AlterColumn("dbo.tbl_bidding_news", "Turnover2YearAbilityFinance", c => c.Long(nullable: false));
            CreateIndex("dbo.tbl_bidding_news", "BiddingPackageId");
            CreateIndex("dbo.tbl_bidding_news", "BiddingPackageOtherId");
            AddForeignKey("dbo.tbl_bidding_news", "BiddingPackageOtherId", "dbo.tbl_bidding_package_other", "Id");
            AddForeignKey("dbo.tbl_bidding_news", "BiddingPackageId", "dbo.tbl_bidding_package", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_bidding_news", "BiddingPackageId", "dbo.tbl_bidding_package");
            DropForeignKey("dbo.tbl_bidding_news", "BiddingPackageOtherId", "dbo.tbl_bidding_package_other");
            DropIndex("dbo.tbl_bidding_news", new[] { "BiddingPackageOtherId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "BiddingPackageId" });
            AlterColumn("dbo.tbl_bidding_news", "Turnover2YearAbilityFinance", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_bidding_news", "BiddingPackageId", c => c.Long(nullable: false));
            DropColumn("dbo.tbl_bidding_news", "YearFinanceSituationAbilityFinance");
            DropColumn("dbo.tbl_bidding_news", "YearOfTurnoverAbilityFinance");
            DropColumn("dbo.tbl_bidding_news", "BiddingPackageOtherId");
            DropTable("dbo.tbl_bidding_package_other");
            CreateIndex("dbo.tbl_bidding_news", "BiddingPackageId");
            AddForeignKey("dbo.tbl_bidding_news", "BiddingPackageId", "dbo.tbl_bidding_package", "Id", cascadeDelete: true);
        }
    }
}
