using System;
using System.Collections.Generic;
using FW.Models;

namespace FW.ViewModels
{
    public class BiddingDetailVM : BaseEntity
    {
        public long? Id { get; set; }
        public long? CompanyProfileId { get; set; }
        public long? BiddingNewsId { get; set; }
        public Decimal Price { get; set; }
        public DateTime BiddingDate { get; set; }
        public bool InvestorSelected { get; set; }
        public int NumberOfDaysImplement { get; set; }

        public Models.BiddingNews BiddingNews { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        public IEnumerable<BiddingDetailFiles> BiddingDetailFiles { get; set; }
        public IEnumerable<CompanyProfile> CompanyProfiles { get; set; }

    }
}
