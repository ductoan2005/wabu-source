using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingDetailFilesConfiguration : CustomEntityTypeConfiguration<BiddingDetailFiles>
    {
        public BiddingDetailFilesConfiguration() : base()
        {
            ToTable("tbl_bidding_detail_file");
            HasKey(u => u.Id);

            Property(p => p.BiddingNewsFileType);
            Property(p => p.BiddingNewsFileTypeName);
            Property(p => p.FileName);
            Property(p => p.FilePath);

            Property(p => p.BiddingDetailId);
            Property(p => p.TechnicalOtherId);
        }
    }
}
