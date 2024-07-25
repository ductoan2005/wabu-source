using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class ConstructionConfiguration : CustomEntityTypeConfiguration<Construction>
    {
        public ConstructionConfiguration() : base()
        {
            ToTable("tbl_construction");
            EntityTypeComment("table Construction");

            //Id
            HasKey(c => c.Id);
            PropertyComment(c => c.Id, "ID Construction de quan ly DB");

            //UserId
            Property(p => p.UserId).IsRequired();
            PropertyComment(p => p.UserId, "User Id");

            //ConstructionName
            Property(p => p.ConstructionName);
            PropertyComment(p => p.ConstructionName, "ten cong trinh");

            //InvestorName
            Property(p => p.InvestorName);
            PropertyComment(p => p.InvestorName, "ten chu dau tu");

            //AddressBuild
            Property(p => p.AddressBuild);
            PropertyComment(p => p.AddressBuild, "dia diem xay dung");

            //Scale
            Property(p => p.Scale);
            PropertyComment(p => p.Scale, "quy mo cong trinh");

            //AcreageBuild
            Property(p => p.AcreageBuild);
            PropertyComment(p => p.AcreageBuild, "dien tich san xay dung");

            //ConstructionDescription
            Property(p => p.ConstructionDescription);
            PropertyComment(p => p.ConstructionDescription, "mo ta cong trinh");

            //ContactName
            Property(p => p.ContactName);
            PropertyComment(p => p.ContactName, "nguoi lien he");

            //ContactPhoneNumber
            Property(p => p.ContactPhoneNumber);
            PropertyComment(p => p.ContactPhoneNumber, "so dt nguoi lien he");

            //IsDisplayContact
            Property(p => p.IsDisplayContact);
            PropertyComment(p => p.IsDisplayContact, "co hien thi lien he khong");

            //AreaId
            Property(p => p.AreaId);
            PropertyComment(p => p.AreaId, "khu vuc cong trinh");

            //BuildingPermit
            Property(p => p.BuildingPermit).IsRequired();
            PropertyComment(p => p.BuildingPermit, "giay phep xay dung");

            //BuildingPermitDate
            Property(p => p.BuildingPermitDate);
            PropertyComment(p => p.BuildingPermitDate, "ngay cap giay phep");

            //ContactEmail
            Property(p => p.ContactEmail);
            PropertyComment(p => p.ContactEmail, "email lien he");

            Property(p => p.Image1FileName);
            PropertyComment(p => p.Image1FileName, "ten hinh anh 1");

            Property(p => p.Image2FileName);
            PropertyComment(p => p.Image2FileName, "ten hinh anh 2");

            Property(p => p.Image3FileName);
            PropertyComment(p => p.Image3FileName, "ten hinh anh 3");

            Property(p => p.Image1FilePath);
            PropertyComment(p => p.Image1FilePath, "duong dan hinh anh 1");

            Property(p => p.Image2FilePath);
            PropertyComment(p => p.Image2FilePath, "duong dan hinh anh 2");

            Property(p => p.Image3FilePath);
            PropertyComment(p => p.Image3FilePath, "duong dan hinh anh 3");

            Property(p => p.ConstructionForm);
            PropertyComment(p => p.ConstructionForm, "Hình thức xây dựng");

            Property(p => p.Basement);
            PropertyComment(p => p.Basement, "tang ham");

            HasMany(cr => cr.BiddingNews)
              .WithRequired(b => b.Construction)
              .HasForeignKey(b => b.ConstructionId)
              .WillCascadeOnDelete(false);
        }
    }
}
