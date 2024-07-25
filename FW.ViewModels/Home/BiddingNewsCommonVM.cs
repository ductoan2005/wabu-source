using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels
{
    public class BiddingNewsCommonVM
    {
        public long? BiddingNewsId { get; set; }
        public string Image { get; set; }
        public string ConstructionName { get; set; }
        public string BiddingPackageName { get; set; }
        public string InvestorName { get; set; }
        public string AreaName { get; set; }
        public int NumberBidder { get; set; }
        public int NumberBidded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? BidCloseDate { get; set; }
        public string BiddingPackageDescription { get; set; }
        public DateTime? NewsApprovalDate { get; set; }
    }
}
