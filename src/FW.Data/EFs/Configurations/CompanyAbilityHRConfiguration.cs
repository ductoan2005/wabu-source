using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class CompanyAbilityHRConfiguration : CustomEntityTypeConfiguration<CompanyAbilityHR>
    {
        public CompanyAbilityHRConfiguration() : base()
        {
            ToTable("tbl_company_ability_hr");
            EntityTypeComment("tbl_company_ability_hr");

            //Id
            HasKey(c => c.Id);

            Property(c => c.FullName);
            Property(c => c.Age);
            Property(c => c.Title);
            Property(c => c.Certificate);
            Property(c => c.School);
            Property(c => c.Branch);
            Property(c => c.Address);
            Property(c => c.PhoneNumber);
            Property(c => c.EvidenceAppointmentStaffFileName);
            Property(c => c.EvidenceLaborContractFileName);
            Property(c => c.EvidenceSimilarCertificatesFileName);
            Property(c => c.EvidenceAppointmentStaffFilePath);
            Property(c => c.EvidenceLaborContractFilePath);
            Property(c => c.EvidenceSimilarCertificatesFilePath);
            Property(c => c.CompanyId);
        }
    }
}
