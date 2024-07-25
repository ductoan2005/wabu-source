using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class CompanyConfiguration : CustomEntityTypeConfiguration<Company>
    {
        public CompanyConfiguration() : base()
        {
            ToTable("tbl_company");
            EntityTypeComment("table company");

            //Id
            HasKey(c => c.Id);
            PropertyComment(c => c.Id, "ID company de quan ly DB");
            
            //CompanyName
            Property(c => c.CompanyName).IsRequired().HasMaxLength(100).IsUnicode();
            PropertyComment(c => c.CompanyName, "Ten company");

            //UserId
            Property(c => c.UserId);

            //Introduction
            Property(c => c.Introduction);

            //RepresentativeName
            Property(c => c.RepresentativeName);

            //Position
            Property(c => c.Position);

            //CompanyAddress
            Property(c => c.CompanyAddress);

            //CompanyPhoneNumber
            Property(c => c.CompanyPhoneNumber);

            //NoBusinessLicense
            Property(c => c.NoBusinessLicense);

            //NoBusinessLicenseFilePath
            Property(c => c.NoBusinessLicensePath);

            //NoBusinessLicenseName
            Property(c => c.NoBusinessLicenseName);

            //FoundedYear
            Property(c => c.FoundedYear);

            //Capital
            Property(c => c.Capital);

            //TaxCode
            Property(c => c.TaxCode);

            //OrganizationalChart file name
            Property(c => c.OrganizationalChartName);

            //OrganizationalChart file path
            Property(c => c.OrganizationalChartPath);

            //ContactName
            Property(c => c.ContactName);

            //ContactPhoneNumber
            Property(c => c.ContactPhoneNumber);

            //Logo
            Property(c => c.Logo);


            //OneStar
            Property(c => c.OneStar);

            //TwoStar
            Property(c => c.TwoStar);

            //ThreeStar
            Property(c => c.ThreeStar);

            //FourStar
            Property(c => c.FourStar);

            //FiveStar
            Property(c => c.FiveStar);

            //Link
            Property(c => c.Link);  //website

            //IsOnOver
            Property(c => c.IsOnOver);

            //TotalBiddedNews
            Property(c => c.TotalBiddedNews);

            //ProjectImplemented
            Property(c => c.ProjectImplemented);

            //ProjectsComplete
            Property(c => c.ProjectsComplete);

            //StringHtml
            Property(c => c.StringHtml);

            //AdvertisingIsOn
            Property(c => c.AdvertisingIsOn);

            //AdvertisingBackgroundImage
            Property(c => c.AdvertisingBackgroundImage);

            //Collection CompanyStaffs
            HasMany(fr => fr.CompanyStaffs)
               .WithRequired(u => u.Company)
               .HasForeignKey(u => u.CompanyId)
               .WillCascadeOnDelete(false);

            //Collection CompanyAbilityExps
            HasMany(fr => fr.CompanyAbilityExps)
               .WithRequired(u => u.Company)
               .HasForeignKey(u => u.CompanyId)
               .WillCascadeOnDelete(false);

            //Collection CompanyAbilityFinances
            HasMany(fr => fr.CompanyAbilityFinances)
               .WithRequired(u => u.Company)
               .HasForeignKey(u => u.CompanyId)
               .WillCascadeOnDelete(false);

            //Collection CompanyAbilityHRs
            HasMany(fr => fr.CompanyAbilityHRs)
               .WithRequired(u => u.Company)
               .HasForeignKey(u => u.CompanyId)
               .WillCascadeOnDelete(false);

            //Collection CompanyAbilityEquipments
            HasMany(fr => fr.CompanyAbilityEquipments)
               .WithRequired(u => u.Company)
               .HasForeignKey(u => u.CompanyId)
               .WillCascadeOnDelete(false);

            //Collection CompanyAbilityEquipments
            HasMany(fr => fr.CompanyProfiles)
               .WithRequired(u => u.Company)
               .HasForeignKey(u => u.CompanyId)
               .WillCascadeOnDelete(false);

            //// common properties(luon phai co)
            ////IsDeleted
            //Property(c => c.IsDeleted).IsRequired();
            //PropertyComment(c => c.IsDeleted, "check delete");
            ////DateInserted
            //CommonPropertyComment(p => p.DateInserted, "Ngay Insert");
            ////DateUpdated
            //CommonPropertyComment(p => p.DateUpdated, "ngay update");
        }
    }
}
