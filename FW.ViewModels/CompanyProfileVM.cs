using FW.Models;
using System;

namespace FW.ViewModels
{
    public class CompanyProfileVM
    {
        public long? Id { get; set; }
        public long? CompanyId { get; set; }
        public string NameProfile { get; set; }
        public string AbilityEquipmentsId { get; set; }
        public string AbilityHRsId { get; set; }
        public string AbilityExpsId { get; set; }
        public string AbilityFinancesId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateInserted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool? IsBidding { get; set; }

        public Company Company { get; set; }
    }
}
