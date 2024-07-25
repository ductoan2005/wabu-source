namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableBiddingDetailFiles_AddNewFieldImageToContruction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_bidding_detail_file",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileAttachOther = c.Binary(),
                        FileAttachProgressScheduleMKT = c.Binary(),
                        FileAttachQuotationMKT = c.Binary(),
                        FileAttachMaterialsUseMKT = c.Binary(),
                        FileAttachDrawingConstructionMKT = c.Binary(),
                        FileAttachWorkSafetyMKT = c.Binary(),
                        FileAttachEnvironmentalSanitationMKT = c.Binary(),
                        FileAttachFireProtectionMKT = c.Binary(),
                        BiddingDetailId = c.Long(nullable: false),
                        TechnicalOtherId = c.Long(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_detail", t => t.BiddingDetailId, cascadeDelete: true)
                .Index(t => t.BiddingDetailId);
            
            AddColumn("dbo.tbl_contruction", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_bidding_detail_file", "BiddingDetailId", "dbo.tbl_bidding_detail");
            DropIndex("dbo.tbl_bidding_detail_file", new[] { "BiddingDetailId" });
            DropColumn("dbo.tbl_contruction", "Image");
            DropTable("dbo.tbl_bidding_detail_file");
        }
    }
}
