using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingNewsBookmarkConfiguration : CustomEntityTypeConfiguration<BiddingNewsBookmark>
    {
        public BiddingNewsBookmarkConfiguration() : base()
        {
            ToTable("tbl_bidding_news_bookmark");

            //Id
            HasKey(u => u.Id);

            Property(u => u.BiddingNewsId);
            //UserId
            Property(p => p.UserId).IsRequired();
            PropertyComment(p => p.UserId, "User Id");

            Property(u => u.BookmarkDate);
        }
    }
}
