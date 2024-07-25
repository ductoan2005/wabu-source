using System;

namespace FW.Models
{
    [Serializable]
    public class BiddingPackageOther : BaseEntity
    {
        public long? Id { get; set; }
        public string BiddingPackageOtherName { get; set; }
    }
}
