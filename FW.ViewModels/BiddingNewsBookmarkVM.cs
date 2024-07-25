using FW.Common.Enum;
using System;

namespace FW.ViewModels
{
    public class BiddingNewsBookmarkVM
    {
        public long? Id { get; set; }
        public long? BiddingNewsId { get; set; }
        public DateTime? BookmarkDate { get; set; }

        public Models.BiddingNews BiddingNews { get; set; }
        public StatusBiddingNewsBookmark StatusBiddingNewsBookmark { get; set; }
    }
}
