using System;
using System.Collections.Generic;

namespace FW.Models
{
    [Serializable]
    public class CompanyAbilityHR : BaseEntity
    {
        public long? Id { get; set; }

        public string FullName { get; set; }
        public string Age { get; set; }
        public string Title { get; set; }
        public string Certificate { get; set; }
        public string School { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string EvidenceLaborContractFilePath { get; set; }
        public string EvidenceSimilarCertificatesFilePath { get; set; }
        public string EvidenceAppointmentStaffFilePath { get; set; }

        public string EvidenceLaborContractFileName { get; set; }
        public string EvidenceSimilarCertificatesFileName { get; set; }
        public string EvidenceAppointmentStaffFileName { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<CompanyAbilityHRDetail> CompanyAbilityHRDetails { get; set; }
    }
}
