using System;

namespace FW.Models
{
    [Serializable]
    public class WorkContent : BaseEntity
    {
        public long? Id { get; set; }
        public string WorkContentName { get; set; }
        public long? BiddingPackageId { get; set; }
        public virtual BiddingPackage BiddingPackage { get; set; }
    }
}
