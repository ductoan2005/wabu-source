using System.ComponentModel.DataAnnotations;

namespace FW.Common.Enum
{
    public enum EContractFormForCreateAndEdit
    {
        //[Display(Name = "Chọn hình thức hợp đồng")]
        //All,

        [Display(Name = "Hợp đồng trọn gói")]
        PackageContract=1,

        [Display(Name = "Hợp đồng đơn giá m² sàn xây dựng")]
        UnitPriceSQM=2,

        [Display(Name = "Hợp đồng đơn giá công việc thực hiện")]
        UnitPriceOfWork=3,
    }
}