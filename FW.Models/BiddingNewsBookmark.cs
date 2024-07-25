using System;

namespace FW.Models
{
    [Serializable]
    public class BiddingNewsBookmark : BaseEntity
    {
        public long? Id { get; set; }
        public long? BiddingNewsId { get; set; }
        public long? UserId { get; set; }
        public DateTime? BookmarkDate { get; set; }

        public virtual BiddingNews BiddingNews { get; set; }
    }
}
