using System;

namespace FW.ViewModels.BiddingNews
{
    public class BiddingNewsDetailVM
    {
        public long? Id { get; set; }
        public long? CompanyProfileId { get; set; }
        public long? BiddingNewsId { get; set; }
        public Decimal Price { get; set; }
        public DateTime BiddingDate { get; set; }
        public bool InvestorSelected { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateInserted { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
