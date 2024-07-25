using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingDetailConfiguration : CustomEntityTypeConfiguration<BiddingDetail>
    {
        public BiddingDetailConfiguration() : base()
        {
            ToTable("tbl_bidding_detail");

            //Id
            HasKey(u => u.Id);

            Property(u => u.CompanyProfileId);
            Property(u => u.BiddingNewsId);
            Property(u => u.BiddingDate);
            Property(u => u.Price);
            Property(u => u.InvestorSelected);
            Property(u => u.NumberOfDaysImplement);

            HasMany(fr => fr.BiddingDetailFiles)
              .WithRequired(u => u.BiddingDetail)
              .HasForeignKey(u => u.BiddingDetailId)
              .WillCascadeOnDelete(false);

        }
    }
}
