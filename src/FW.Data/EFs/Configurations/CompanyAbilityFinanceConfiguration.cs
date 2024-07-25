using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class CompanyAbilityFinanceConfiguration : CustomEntityTypeConfiguration<CompanyAbilityFinance>
    {
        public CompanyAbilityFinanceConfiguration() : base()
        {
            ToTable("tbl_company_ability_finance");

            //Id
            HasKey(c => c.Id);

            Property(c => c.YearDeclare);
            Property(c => c.TotalAssets);
            Property(c => c.TotalLiabilities);
            Property(c => c.ShortTermAssets);
            Property(c => c.TotalCurrentLiabilities);
            Property(c => c.Revenue);
            Property(c => c.ProfitBeforeTax);
            Property(c => c.ProfitAfterTax);
            Property(c => c.EvidenceAuditReportFileName);
            Property(c => c.EvidenceCertificationTaxFileName);
            Property(c => c.EvidenceCheckSettlementFileName);
            Property(c => c.EvidenceDeclareTaxFileName);
            Property(c => c.EvidenceAuditReportFilePath);
            Property(c => c.EvidenceCertificationTaxFilePath);
            Property(c => c.EvidenceCheckSettlementFilePath);
            Property(c => c.EvidenceDeclareTaxFilePath);
            Property(c => c.CompanyId);
        }
    }
}
