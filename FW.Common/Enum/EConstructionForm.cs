using System.ComponentModel.DataAnnotations;

namespace FW.Common.Enum
{
    public enum EConstructionForm
    {
        [Display(Name = "Xây dựng mới")] 
        NewConstruction,

        [Display(Name = "Cải tạo sửa chữa")]
        RenovationsRepair,

        [Display(Name = "Trang trí nội thất")]
        Interrior
    }
}
