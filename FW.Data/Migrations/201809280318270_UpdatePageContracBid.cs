namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePageContracBid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContract");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContract", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract", c => c.Binary());
        }
    }
}
