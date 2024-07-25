using System.Collections.Generic;
using System.Web;

namespace FW.ViewModels.BiddingNews
{
    public class PrintInfoBiddingVM
    {
        public long? Id { get; set; }

        public long? CompanyProfileId { get; set; }

        public long? BiddingNewsId { get; set; }

        public decimal Price { get; set; }

        public HttpPostedFileBase FileAttachProgressScheduleMKT { get; set; }

        public HttpPostedFileBase FileAttachQuotationMKT { get; set; }

        public HttpPostedFileBase FileAttachMaterialsUseMKT { get; set; }

        public HttpPostedFileBase FileAttachDrawingConstructionMKT { get; set; }

        public HttpPostedFileBase FileAttachWorkSafetyMKT { get; set; }

        public HttpPostedFileBase FileAttachEnvironmentalSanitationMKT { get; set; }

        public HttpPostedFileBase FileAttachFireProtectionMKT { get; set; }

        public List<string> TechnicalRequirementNameList { get; set; }

        public List<long> TechnicalOtherIdList { get; set; }

        public List<HttpPostedFileBase> OtherFiles { get; set; }

        public string NameProfile { get; set; }

        public string CompanyName { get; set; }

        public int NumberOfDaysImplement { get; set; }
    }

    public class OtherFilesModel
    {
        public string TechnicalRequirementName { get; set; }

        HttpPostedFileBase OtherFiles { get; set; }
    }
}
