using System;
using System.Collections.Generic;

namespace FW.Models
{
    [Serializable]
    public class Construction : BaseEntity
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string ConstructionName { get; set; }
        public string InvestorName { get; set; }
        public string AddressBuild { get; set; }
        public int Scale { get; set; }
        public int AcreageBuild { get; set; }
        public string ConstructionDescription { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public bool IsDisplayContact { get; set; }
        public long? AreaId { get; set; }
        public string BuildingPermit { get; set; }
        public DateTime? BuildingPermitDate { get; set; }
        public string ContactEmail { get; set; }
        public string Image1FileName { get; set; }
        public string Image2FileName { get; set; }
        public string Image3FileName { get; set; }
        public string Image1FilePath { get; set; }
        public string Image2FilePath { get; set; }
        public string Image3FilePath { get; set; }
        public byte ConstructionForm { get; set; }

        public int? Basement { get; set; }

        public virtual Users User { get; set; }
        public virtual Area Area { get; set; }
        public virtual ICollection<BiddingNews> BiddingNews { get; set; }
    }
}
