using System;

namespace FW.ViewModels
{
    public class CompanyAbilityHrDetailVM
    {
        public long? Id { get; set; }

        public DateTime? FromYear { get; set; } = DateTime.MinValue;
        public DateTime? ToYear { get; set; } = DateTime.MinValue;
        public string ProjectSimilar { get; set; }
        public string PositionSimilar { get; set; }
        public string ExpTechnical { get; set; }
        public string FromYearString { get; set; }
        public string ToYearString { get; set; }

        public long? CompanyAbilityHRId { get; set; }

        public bool? IsDeleted { get; set; }
        // ngay insert
        public virtual DateTime? DateInserted { get; set; }
        // ngay update
        public virtual DateTime? DateUpdated { get; set; }
    }
}
