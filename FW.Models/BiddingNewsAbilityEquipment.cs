using System;

namespace FW.Models
{
    [Serializable]
    public class BiddingNewsAbilityEquipment : BaseEntity
    {
        public long? Id { get; set; }
        public string EquipmentName { get; set; }
        public string PowerEquipment { get; set; }
        public int QuantityEquipment { get; set; }
        public bool? IsAccreditation { get; set; }
        public long? BiddingNewsId { get; set; }
        public virtual BiddingNews BiddingNews { get; set; }
    }
}
