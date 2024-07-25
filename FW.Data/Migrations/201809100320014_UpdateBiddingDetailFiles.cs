namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBiddingDetailFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_detail_file", "AttachOtherFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachOtherFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFileName", c => c.String());
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachOther");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachProgressScheduleMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachQuotationMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachMaterialsUseMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachDrawingConstructionMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachWorkSafetyMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachEnvironmentalSanitationMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachFireProtectionMKT");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachOtherFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachProgressScheduleMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachQuotationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachMaterialsUseMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachDrawingConstructionMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachWorkSafetyMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachEnvironmentalSanitationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachFireProtectionMKTFileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachFireProtectionMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachEnvironmentalSanitationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachWorkSafetyMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachDrawingConstructionMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachMaterialsUseMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachQuotationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachProgressScheduleMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachOtherFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachFireProtectionMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachEnvironmentalSanitationMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachWorkSafetyMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachDrawingConstructionMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachMaterialsUseMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachQuotationMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachProgressScheduleMKT", c => c.Binary());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachOther", c => c.Binary());
            DropColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachOtherFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachFireProtectionMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachEnvironmentalSanitationMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachWorkSafetyMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachDrawingConstructionMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachMaterialsUseMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachQuotationMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachProgressScheduleMKTFilePath");
            DropColumn("dbo.tbl_bidding_detail_file", "AttachOtherFilePath");
        }
    }
}
