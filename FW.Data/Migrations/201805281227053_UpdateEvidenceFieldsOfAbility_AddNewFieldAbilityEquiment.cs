namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEvidenceFieldsOfAbility_AddNewFieldAbilityEquiment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_company_ability_equipment", "IsBiddingOwner", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_company_ability_equipment", "IsBiddingHire", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_company_ability_equipment", "IsBiddingSpecialManufacture", c => c.Boolean(nullable: false));

            //AlterColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_equipment SET EvidenceSaleContractTmp = Convert(varbinary, EvidenceSaleContract)");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract");
            RenameColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractTmp", "EvidenceSaleContract");



            //AlterColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_equipment SET EvidenceInspectionRecordsTmp = Convert(varbinary, EvidenceInspectionRecords)");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords");
            RenameColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsTmp", "EvidenceInspectionRecords");

            //AlterColumn("dbo.tbl_company_ability_exp", "EvidenceContract", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_exp SET EvidenceContractTmp = Convert(varbinary, EvidenceContract)");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContract");
            RenameColumn("dbo.tbl_company_ability_exp", "EvidenceContractTmp", "EvidenceContract");

            //AlterColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_exp SET EvidenceContractLiquidationTmp = Convert(varbinary, EvidenceContractLiquidation)");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation");
            RenameColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationTmp", "EvidenceContractLiquidation");

            //AlterColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_exp SET EvidenceBuildingPermitTmp = Convert(varbinary, EvidenceBuildingPermit)");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit");
            RenameColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitTmp", "EvidenceBuildingPermit");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceCheckSettlementTmp = Convert(varbinary, EvidenceCheckSettlement)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement");
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementTmp", "EvidenceCheckSettlement");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceDeclareTaxTmp = Convert(varbinary, EvidenceDeclareTax)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax");
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxTmp", "EvidenceDeclareTax");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceCertificationTaxTmp = Convert(varbinary, EvidenceCertificationTax)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax");
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxTmp", "EvidenceCertificationTax");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceAuditReportTmp = Convert(varbinary, EvidenceAuditReport)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport");
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportTmp", "EvidenceAuditReport");

            //AlterColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_hr SET EvidenceLaborContractTmp = Convert(varbinary, EvidenceLaborContract)");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract");
            RenameColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractTmp", "EvidenceLaborContract");

            //AlterColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_hr SET EvidenceSimilarCertificatesTmp = Convert(varbinary, EvidenceSimilarCertificates)");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates");
            RenameColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesTmp", "EvidenceSimilarCertificates");

            //AlterColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff", c => c.Binary());
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffTmp", c => c.Binary());
            Sql("Update dbo.tbl_company_ability_hr SET EvidenceAppointmentStaffTmp = Convert(varbinary, EvidenceAppointmentStaff)");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff");
            RenameColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffTmp", "EvidenceAppointmentStaff");
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff", c => c.String());
            RenameColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff", "EvidenceAppointmentStaffTmp");
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaff", c => c.String());
            Sql("Update dbo.tbl_company_ability_hr SET EvidenceAppointmentStaff = Convert(string, EvidenceAppointmentStaffTmp)");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceAppointmentStaffTmp");

            //AlterColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates", c => c.String());
            RenameColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates", "EvidenceSimilarCertificatesTmp");
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificates", c => c.String());
            Sql("Update dbo.tbl_company_ability_hr SET EvidenceSimilarCertificates = Convert(string, EvidenceSimilarCertificatesTmp)");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceSimilarCertificatesTmp");

            //AlterColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract", c => c.String());
            RenameColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract", "EvidenceLaborContractTmp");
            AddColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContract", c => c.String());
            Sql("Update dbo.tbl_company_ability_hr SET EvidenceLaborContract = Convert(string, EvidenceLaborContractTmp)");
            DropColumn("dbo.tbl_company_ability_hr", "EvidenceLaborContractTmp");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport", c => c.String());
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport", "EvidenceAuditReportTmp");
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReport", c => c.String());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceAuditReport = Convert(string, EvidenceAuditReportTmp)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceAuditReportTmp");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax", c => c.String());
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax", "EvidenceCertificationTaxTmp");
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTax", c => c.String());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceCertificationTax = Convert(string, EvidenceCertificationTaxTmp)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCertificationTaxTmp");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax", c => c.String());
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax", "EvidenceDeclareTaxTmp");
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTax", c => c.String());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceDeclareTax = Convert(string, EvidenceDeclareTaxTmp)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceDeclareTaxTmp");

            //AlterColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement", c => c.String());
            RenameColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement", "EvidenceCheckSettlementTmp");
            AddColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlement", c => c.String());
            Sql("Update dbo.tbl_company_ability_finance SET EvidenceCheckSettlement = Convert(string, EvidenceCheckSettlementTmp)");
            DropColumn("dbo.tbl_company_ability_finance", "EvidenceCheckSettlementTmp");

            //AlterColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit", c => c.String());
            RenameColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit", "EvidenceBuildingPermitTmp");
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermit", c => c.String());
            Sql("Update dbo.tbl_company_ability_exp SET EvidenceBuildingPermit = Convert(string, EvidenceBuildingPermitTmp)");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceBuildingPermitTmp");

            //AlterColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation", c => c.String());
            RenameColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation", "EvidenceContractLiquidationTmp");
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidation", c => c.String());
            Sql("Update dbo.tbl_company_ability_exp SET EvidenceContractLiquidation = Convert(string, EvidenceContractLiquidationTmp)");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractLiquidationTmp");

            //AlterColumn("dbo.tbl_company_ability_exp", "EvidenceContract", c => c.String());
            RenameColumn("dbo.tbl_company_ability_exp", "EvidenceContract", "EvidenceContractTmp");
            AddColumn("dbo.tbl_company_ability_exp", "EvidenceContract", c => c.String());
            Sql("Update dbo.tbl_company_ability_exp SET EvidenceContract = Convert(string, EvidenceContractTmp)");
            DropColumn("dbo.tbl_company_ability_exp", "EvidenceContractTmp");

            //AlterColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords", c => c.String());
            RenameColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords", "EvidenceInspectionRecordsTmp");
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecords", c => c.String());
            Sql("Update dbo.tbl_company_ability_equipment SET EvidenceInspectionRecords = Convert(string, EvidenceInspectionRecordsTmp)");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceInspectionRecordsTmp");

            //AlterColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract", c => c.String());
            RenameColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract", "EvidenceSaleContractTmp");
            AddColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContract", c => c.String());
            Sql("Update dbo.tbl_company_ability_equipment SET EvidenceSaleContract = Convert(string, EvidenceSaleContractTmp)");
            DropColumn("dbo.tbl_company_ability_equipment", "EvidenceSaleContractTmp");

            DropColumn("dbo.tbl_company_ability_equipment", "IsBiddingSpecialManufacture");
            DropColumn("dbo.tbl_company_ability_equipment", "IsBiddingHire");
            DropColumn("dbo.tbl_company_ability_equipment", "IsBiddingOwner");
        }
    }
}
