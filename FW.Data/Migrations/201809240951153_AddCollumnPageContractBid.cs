namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollumnPageContractBid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesFilePath", c => c.String());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffFilePath");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesFilePath");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractFilePath");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportFilePath");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxFilePath");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxFilePath");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementFilePath");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitFilePath");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationFilePath");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractFilePath");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsFilePath");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractFilePath");
        }
    }
}
