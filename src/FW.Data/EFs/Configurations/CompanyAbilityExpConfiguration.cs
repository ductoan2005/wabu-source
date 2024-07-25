using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class CompanyAbilityExpConfiguration : CustomEntityTypeConfiguration<CompanyAbilityExp>
    {
        public CompanyAbilityExpConfiguration() : base()
        {
            ToTable("tbl_company_ability_exp");

            //Id
            HasKey(c => c.Id);

            Property(c => c.ProjectName);
            Property(c => c.InvestorName);
            Property(c => c.InvestorAddress);
            Property(c => c.InvestorPhoneNumber);
            Property(c => c.ContructionType);
            Property(c => c.ProjectScale);
            Property(c => c.ContractName);
            Property(c => c.ContractSignDate);
            Property(c => c.ContractCompleteDate);
            Property(c => c.ContractPrices);
            Property(c => c.ProjectDescription);
            Property(c => c.EvidenceBuildingPermitFilePath);
            Property(c => c.EvidenceContractFilePath);
            Property(c => c.EvidenceContractLiquidationFilePath);
            Property(c => c.EvidenceBuildingPermitFileName);
            Property(c => c.EvidenceContractFileName);
            Property(c => c.EvidenceContractLiquidationFileName);
            Property(c => c.CompanyId);
        }
    }
}
