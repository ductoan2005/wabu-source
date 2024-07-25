using System;
using System.Collections.Generic;

namespace FW.Models
{
    public class BiddingDetail : BaseEntity
    {
        public long? Id { get; set; }
        public long? CompanyProfileId { get; set; }
        public long? BiddingNewsId { get; set; }
        public Decimal Price { get; set; }
        public DateTime BiddingDate { get; set; }
        public bool InvestorSelected { get; set; }
        public int NumberOfDaysImplement { get; set; }

        public virtual BiddingNews BiddingNews { get; set; }
        public virtual CompanyProfile CompanyProfile { get; set; }
        public virtual ICollection<BiddingDetailFiles> BiddingDetailFiles { get; set; }
    }
}
