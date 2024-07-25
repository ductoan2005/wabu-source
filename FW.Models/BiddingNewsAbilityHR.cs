using System;

namespace FW.Models
{
    [Serializable]
    public class BiddingNewsAbilityHR : BaseEntity
    {
        public long? Id { get; set; }
        public string JobPosition { get; set; }
        public string QualificationRequired { get; set; }
        public int YearExp { get; set; }
        public int NumberRequest { get; set; }
        public string SimilarProgram { get; set; }
        public long? BiddingNewsId { get; set; }
        public virtual BiddingNews BiddingNews { get; set; }
    }
}
