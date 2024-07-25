namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesFileName", c => c.String());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachOtherFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachProgressScheduleMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachQuotationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachMaterialsUseMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachDrawingConstructionMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachWorkSafetyMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachEnvironmentalSanitationMKTFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_detail_file", "FileAttachFireProtectionMKTFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachFireProtectionMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachEnvironmentalSanitationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachWorkSafetyMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachDrawingConstructionMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachMaterialsUseMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachQuotationMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachProgressScheduleMKTFileName");
            DropColumn("dbo.tbl_bidding_detail_file", "FileAttachOtherFileName");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffFileName");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesFileName");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractFileName");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportFileName");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxFileName");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxFileName");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementFileName");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitFileName");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationFileName");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractFileName");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsFileName");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractFileName");
            DropColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKFileName");
        }
    }
}
