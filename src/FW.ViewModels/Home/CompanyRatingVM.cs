using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels
{
    public class CompanyRatingVM
    {
        public string CompanyName { get; set; }
        public DateTime? DateInserted { get; set; }
        public int TotalBiddedNews { get; set; }
        public int ProjectImplemented { get; set; }
        public int ProjectsComplete { get; set; }
        public string Introduction { get; set; }
        public long IsOnOver { get; set; }
        public string Link { get; set; }
        public string Logo { get; set; }
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
        public double RatingStar { get; set; }
    }
}
