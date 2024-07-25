using System.ComponentModel.DataAnnotations;

namespace FW.Common.Enum
{
    public enum StatusInvestor
    {
        [Display(Name = "Mở thầu")]//vang
        Opening,
        [Display(Name = "Đóng thầu")]//xam
        Finished,
        [Display(Name = "Đánh giá")]//xanh la
        Evaluate
    }

    public enum StatusContractor
    {
        [Display(Name = "Thương thảo")]//xanh la
        Negotiation,
        [Display(Name = "Thực hiện")]//vang
        Implementing,
        [Display(Name = "Đóng thầu")]//xam
        Finished
    }

    public enum StatusBidding
    {
        [Display(Name = "Tất cả")]
        All,
        [Display(Name = "Đang Mở thầu")]//green
        Opening,
        [Display(Name = "Đang Thương thảo")]//yellow
        Negotiation,
        [Display(Name = "Đang Thực hiện")]//red
        Implementing,
        [Display(Name = "Đã Kết thúc")]//gray
        Finished
    }

    public enum StatusBiddingNewsBookmark
    {
        [Display(Name = "Tất cả")]
        All,
        [Display(Name = "Đang Mở thầu")]//green
        Opening,
        [Display(Name = "Đã Kết thúc")]//gray
        Finished
    }

    public enum StatusDelete
    {
        Success,
        Error,
        Existed
    }

    public enum StatusAdd
    {
        Success,
        Error,
        Existed
    }
}
