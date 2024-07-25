using System;
using System.Collections.Generic;
using System.Web;
using FW.Models;

namespace FW.ViewModels
{
    public class CompanyAbilityHRVM
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

        public HttpPostedFileBase EvidenceLaborContractFile { get; set; }
        public HttpPostedFileBase EvidenceSimilarCertificatesFile { get; set; }
        public HttpPostedFileBase EvidenceAppointmentStaffFile { get; set; }

        public long? CompanyId { get; set; }
        public List<CompanyAbilityHrDetailVM> CompanyAbilityHRDetails { get; set; }

    }
}
