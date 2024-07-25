using System;

namespace FW.Models
{
    [Serializable]
    public class CompanyAbilityHRDetail : BaseEntity
    {
        public long? Id { get; set; }

        public DateTime? FromYear { get; set; }
        public DateTime? ToYear { get; set; }
        public string ProjectSimilar { get; set; }
        public string PositionSimilar { get; set; }
        public string ExpTechnical { get; set; }

        public long? CompanyAbilityHRId { get; set; }
        public virtual CompanyAbilityHR CompanyAbilityHR { get; set; }
    }
}
