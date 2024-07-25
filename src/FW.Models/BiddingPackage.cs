using System;
using System.Collections.Generic;

namespace FW.Models
{
    [Serializable]
    public class BiddingPackage : BaseEntity
    {
        public long? Id { get; set; }
        public byte BiddingPackageType { get; set; }
        public string BiddingPackageName { get; set; }
        //public virtual ICollection<WorkContent> WorkContents { get; set; }
        //public virtual ICollection<WorkContentOther> WorkContentOthers { get; set; }
    }
}
