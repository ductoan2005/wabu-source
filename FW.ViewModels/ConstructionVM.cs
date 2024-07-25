using FW.Common.Enum;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace FW.ViewModels
{
    public class ConstructionVM
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string ConstructionName { get; set; }
        public string InvestorName { get; set; }
        public string AddressBuild { get; set; }
        public int Scale { get; set; }
        public string StrScale { get; set; }
        public int? AcreageBuild { get; set; }
        public string StrAcreageBuild { get; set; }

        public string ConstructionDescription { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public bool IsDisplayContact { get; set; }
        public long? AreaId { get; set; }

        public string StrAreaId { get; set; }

        public string BuildingPermit { get; set; }
        public DateTime? BuildingPermitDate { get; set; }
        public string BuildingPermitDateTime { get; set; }
        public string ContactEmail { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateInserted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public HttpPostedFileBase ImageFile1 { get; set; }
        public HttpPostedFileBase ImageFile2 { get; set; }
        public HttpPostedFileBase ImageFile3 { get; set; }
        public string Image1FileName { get; set; }
        public string Image2FileName { get; set; }
        public string Image3FileName { get; set; }
        public string Image1FilePath { get; set; }
        public string Image2FilePath { get; set; }
        public string Image3FilePath { get; set; }
        public byte ConstructionForm { get; set; }
        public string StrConstructionForm { get; set; }

        public int? Basement { get; set; }
        public string StrBasement { get; set; }

        public EConstructionForm EnumConstructionForm { get; set; }

        public virtual Users Users { get; set; }
        public virtual Area Area { get; set; }
        public List<Models.BiddingNews> BiddingNews { get; set; }
    }
}
