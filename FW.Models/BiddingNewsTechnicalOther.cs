using System;

namespace FW.Models
{
    [Serializable]
    public class BiddingNewsTechnicalOther : BaseEntity
    {
        public long? Id { get; set; }
        public string TechnicalRequirementName { get; set; }
        public long? BiddingNewsId { get; set; }
        public virtual BiddingNews BiddingNews { get; set; }
    }
}
