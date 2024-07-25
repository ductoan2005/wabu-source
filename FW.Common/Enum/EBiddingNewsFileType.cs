
using System.ComponentModel.DataAnnotations;

namespace FW.Common.Enum
{
    public enum EBiddingNewsFileType
    {
        ProgressSchedule,
        [Display(Name = "Bảng báo giá")]
        Quotation,
        MaterialsUse,
        DrawingConstruction,
        WorkSafety,
        EnvironmentalSanitation,
        FireProtection,
        Other
    }
}
