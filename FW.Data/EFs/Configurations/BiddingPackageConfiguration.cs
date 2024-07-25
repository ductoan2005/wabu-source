using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingPackageConfiguration : CustomEntityTypeConfiguration<BiddingPackage>
    {
        public BiddingPackageConfiguration() : base()
        {
            ToTable("tbl_bidding_package");
            EntityTypeComment("table goi thau");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "goi thau ID");

            Property(p => p.BiddingPackageType);

            //BiddingPackageId
            Property(p => p.BiddingPackageName);
            PropertyComment(p => p.BiddingPackageName, "ten goi thau");

            //HasMany(p => p.WorkContents)
            //    .WithRequired(p => p.BiddingPackage)
            //    .HasForeignKey(p => p.BiddingPackageId)
            //    .WillCascadeOnDelete(false);

            //HasMany(p => p.WorkContentOthers)
            //   .WithRequired(p => p.BiddingPackage)
            //   .HasForeignKey(p => p.BiddingPackageId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
