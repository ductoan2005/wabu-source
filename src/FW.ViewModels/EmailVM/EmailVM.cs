using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels.EmailVM
{
    public class EmailVM
    {
        public string FullName { get; set; }
        public string BiddingPackageName { get; set; }
        public string CompanyName { get; set; }
        public long contructionid { get; set; }
        public string ConstructionName { get; set; }
        public string Email { get; set; }
        public string InvestorEmail { get; set; }
        public string InvestorPhone { get; set; }
        public long? BiddingPackageId { get; set; }
        public long? UserIdRole3 { get; set; }
        public long? UserIdRole2 { get; set; }
        public string InvestorName { get; set; }
        public long? BiddingNewsId { get; set; }
    }
}
