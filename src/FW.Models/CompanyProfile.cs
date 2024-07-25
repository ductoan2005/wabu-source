using System;

namespace FW.Models
{
    [Serializable]
    public class CompanyProfile : BaseEntity
    {
        public long? Id { get; set; }
        public long? CompanyId { get; set; }
        public string NameProfile { get; set; }
        public string AbilityEquipmentsId { get; set; }
        public string AbilityHRsId { get; set; }
        public string AbilityExpsId { get; set; }
        public string AbilityFinancesId { get; set; }

        public virtual Company Company { get; set; }
    }
}
