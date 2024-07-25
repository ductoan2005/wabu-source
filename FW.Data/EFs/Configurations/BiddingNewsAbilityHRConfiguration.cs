using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingNewsAbilityHRConfiguration : CustomEntityTypeConfiguration<BiddingNewsAbilityHR>
    {
        public BiddingNewsAbilityHRConfiguration() : base()
        {
            ToTable("tbl_bidding_news_ability_hr");
            EntityTypeComment("table bidding_news_ability_hr");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "bidding_news_ability_hr ID");

            //BiddingNewsId
            Property(u => u.BiddingNewsId);
            PropertyComment(u => u.BiddingNewsId, "tin thau id");

            //JobPosition
            Property(u => u.JobPosition);
            PropertyComment(u => u.JobPosition, "vi tri cong viec");

            //QualificationRequired
            Property(u => u.QualificationRequired);
            PropertyComment(u => u.QualificationRequired, "");

            //YearExp
            Property(u => u.YearExp);
            PropertyComment(u => u.YearExp, "nam kinh nghiem");

            //NumberRequest
            Property(u => u.NumberRequest);
            PropertyComment(u => u.NumberRequest, "so luong yeu cau");

            //SimilarProgram
            Property(u => u.SimilarProgram);
            PropertyComment(u => u.SimilarProgram, "chuong trinh tuong ung");
        }
    }
}
