using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class CompanyAbilityEquipmentConfiguration : CustomEntityTypeConfiguration<CompanyAbilityEquipment>
    {
        public CompanyAbilityEquipmentConfiguration() : base()
        {
            ToTable("tbl_company_ability_equipment");
            EntityTypeComment("tbl_company_ability_equipment");

            //Id
            HasKey(c => c.Id);

            Property(c => c.EquipmentType);
            Property(c => c.Quantity);
            Property(c => c.Capacity);
            Property(c => c.Function);
            Property(c => c.NationalProduction);
            Property(c => c.YearManufacture);
            Property(c => c.QualityUse);
            Property(c => c.Source);
            Property(c => c.EvidenceInspectionRecordsFilePath);
            Property(c => c.EvidenceSaleContractFilePath);
            Property(c => c.EvidenceInspectionRecordsFileName);
            Property(c => c.EvidenceSaleContractFileName);
            Property(c => c.IsBiddingHire);
            Property(c => c.IsBiddingOwner);
            Property(c => c.IsBiddingSpecialManufacture);
            Property(c => c.CompanyId);
        }
    }
}
