using System;

namespace FW.Models
{
    [Serializable]
    public class WorkContentOther : BaseEntity
    {
        public long? Id { get; set; }
        public string WorkContentOtherName { get; set; }
        public long? BiddingPackageId { get; set; }
        public virtual BiddingPackage BiddingPackage { get; set; }
    }
}