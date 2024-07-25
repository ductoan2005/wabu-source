using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class CompanyProfileConfiguration : CustomEntityTypeConfiguration<CompanyProfile>
    {
        public CompanyProfileConfiguration() : base()
        {
            ToTable("tbl_company_profile");
            EntityTypeComment("table ho so thau");

            //Id
            HasKey(u => u.Id);

            Property(u => u.CompanyId);
            Property(u => u.NameProfile);
            Property(u => u.AbilityEquipmentsId);
            Property(u => u.AbilityHRsId);
            Property(u => u.AbilityExpsId);
            Property(u => u.AbilityFinancesId);
        }
    }
}
