namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_BiddingNewsFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_detail_file", "BiddingNewsFileType", c => c.Byte(nullable: false));
            AddColumn("dbo.tbl_bidding_detail_file", "BiddingNewsFileTypeName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FilePath", c => c.String());
            DropColumn("dbo.tbl_bidding_detail_file", "AttachOtherFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachOtherFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachOtherFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachOtherFilePath", c => c.String());
            DropColumn("dbo.tbl_bidding_detail_file", "FilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "FileName");
            DropColumn("dbo.tbl_bidding_detail_file", "BiddingNewsFileTypeName");
            DropColumn("dbo.tbl_bidding_detail_file", "BiddingNewsFileType");
        }
    }
}
