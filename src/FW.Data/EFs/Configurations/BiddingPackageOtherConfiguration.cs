using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingPackageOtherConfiguration : CustomEntityTypeConfiguration<BiddingPackageOther>
    {
        public BiddingPackageOtherConfiguration() : base()
        {
            ToTable("tbl_bidding_package_other");
            EntityTypeComment("table goi thau khac");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "goi thau ID");

            //BiddingPackageId
            Property(p => p.BiddingPackageOtherName);
            PropertyComment(p => p.BiddingPackageOtherName, "ten goi thau khac");
        }
    }
}
