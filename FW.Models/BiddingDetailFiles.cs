using System;

namespace FW.Models
{
    [Serializable]
    public class BiddingDetailFiles : BaseEntity
    {
        public long? Id { get; set; }
        public byte BiddingNewsFileType { get; set; }
        public string BiddingNewsFileTypeName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public long? BiddingDetailId { get; set; }
        public long? TechnicalOtherId { get; set; }
        public virtual BiddingDetail BiddingDetail { get; set; }
    }
}
