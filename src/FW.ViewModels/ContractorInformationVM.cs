using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FW.ViewModels
{
    public class ContractorInformationVM
    {
        public long? Id { get; set; }
        public long UserId { get; set; }
        //public string CompanyCode { get; set; }
        public string Namecompany { get; set; }
        public bool? IsDeleted { get; set; }
        public string Image { get; set; }

        public DateTime? DateUpdated { get; set; }
        public string Contentcompany { get; set; }
        public string Legalrepresentative { get; set; }
        public string Position { get; set; }
        public string Addresscompany { get; set; }
        public string Phonecompany { get; set; }
        public string Yearestablish { get; set; }
        public string Invalidcapital { get; set; }
        public string Taxid { get; set; }

        public string BussinessLicense { get; set; }
        public string NoBusinessLicensePath { get; set; } // GPKD file path
        public string NoBusinessLicenseName { get; set; }
        public string OrganizationalChartName { get; set; } // so do to chuc file name
        public string OrganizationalChartPath { get; set; } // so do to chuc file path

        public string CompanyStaff { get; set; }
        public IEnumerable<CompanyStaffVM> Staffs { get; set; }
    }
}
