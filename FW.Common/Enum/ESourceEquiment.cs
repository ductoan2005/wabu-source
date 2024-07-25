using System;
using System.ComponentModel.DataAnnotations;

namespace FW.Common.Enum
{
    public enum ESourceEquiment
    {
        [Display(Name = "Sở hữu của nhà thầu")]
        IsBiddingOwner = 1,

        [Display(Name = "Thuê mượn")]
        IsBiddingHire = 2,

        [Display(Name = "Chế tạo đặc biệt")]
        IsBiddingSpecialManufacture = 3,
    }
}