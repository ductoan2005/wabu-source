using System;
using FW.Models;

namespace FW.ViewModels
{
    public class BiddingDetailFilesVM : BaseEntity
    {
        public long? Id { get; set; }
        public string AttachOtherFilePath { get; set; }
        public string AttachProgressScheduleMKTFilePath { get; set; }
        public string AttachQuotationMKTFilePath { get; set; }
        public string AttachMaterialsUseMKTFilePath { get; set; }
        public string AttachDrawingConstructionMKTFilePath { get; set; }
        public string AttachWorkSafetyMKTFilePath { get; set; }
        public string AttachEnvironmentalSanitationMKTFilePath { get; set; }
        public string AttachFireProtectionMKTFilePath { get; set; }

        public string AttachOtherFileName { get; set; }
        public string AttachProgressScheduleMKTFileName { get; set; }
        public string AttachQuotationMKTFileName { get; set; }
        public string AttachMaterialsUseMKTFileName { get; set; }
        public string AttachDrawingConstructionMKTFileName { get; set; }
        public string AttachWorkSafetyMKTFileName { get; set; }
        public string AttachEnvironmentalSanitationMKTFileName { get; set; }
        public string AttachFireProtectionMKTFileName { get; set; }

        public long? BiddingDetailId { get; set; }
        public long? TechnicalOtherId { get; set; }
        public virtual BiddingDetail BiddingDetail { get; set; }
    }
}
