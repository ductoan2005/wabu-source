using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class BiddingNewsAbilityEquipmentConfiguration : CustomEntityTypeConfiguration<BiddingNewsAbilityEquipment>
    {
        public BiddingNewsAbilityEquipmentConfiguration() : base()
        {
            ToTable("tbl_bidding_news_ability_equipment");
            EntityTypeComment("table bidding_news_ability_equipment");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "bidding_news_ability_equipment ID");

            //BiddingNewsId
            Property(u => u.BiddingNewsId).IsRequired();
            PropertyComment(u => u.BiddingNewsId, "tin thau id");

            //EquipmentName
            Property(u => u.EquipmentName);
            PropertyComment(u => u.EquipmentName, "ten thiet bi");

            //QuantityEquipment
            Property(u => u.QuantityEquipment);
            PropertyComment(u => u.QuantityEquipment, "so luong thiet bi");

            //IsAccreditation
            Property(u => u.IsAccreditation);
            PropertyComment(u => u.IsAccreditation, "");
        }
    }
}
