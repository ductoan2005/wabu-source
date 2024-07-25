using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingNewsTechnicalOtherConfiguration : CustomEntityTypeConfiguration<BiddingNewsTechnicalOther>
    {
        public BiddingNewsTechnicalOtherConfiguration() : base()
        {
            ToTable("tbl_bidding_news_technical_other");
            EntityTypeComment("table ky thuat khac");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "ky thuat khac ID");

            //TechnicalRequirementName
            Property(p => p.TechnicalRequirementName);

            //BiddingNewsId
            Property(p => p.BiddingNewsId);
        }
    }
}
