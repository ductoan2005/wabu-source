using System.ComponentModel.DataAnnotations;

namespace FW.Common.Enum
{
    public enum EBiddingPackageName
    {
        [Display(Name = "Tất cả gói thầu")]
        All,

        [Display(Name = "Thiết kế bản vẽ thi công")]
        DesignDrawings,

        [Display(Name = "Tư vấn giám sát")]
        SupervisionConsultants,

        [Display(Name = "Thi công xây dựng")]
        Construction,

        [Display(Name = "Khác")]
        Other
    }

    public enum EBiddingPackageNameForCreateAndEdit
    {
        //[Display(Name = "Chọn gói thầu")]
        //All,

        [Display(Name = "Thiết kế bản vẽ thi công")]
        DesignDrawings = 1,

        [Display(Name = "Tư vấn giám sát")]
        SupervisionConsultants = 2,

        [Display(Name = "Thi công xây dựng")]
        Construction = 3,

        [Display(Name = "Khác")]
        Other = 4
    }
}