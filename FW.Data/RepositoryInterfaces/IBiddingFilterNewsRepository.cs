using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using FW.ViewModels.BiddingFilterNews;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingFilterNewsRepository : IRepository<BiddingNews, long?>
    {
        /// <summary>
        /// BiddingFilter to admin
        /// </summary>
        /// <param name="DateInserted"></param>
        /// <param name="BidStartDate"></param>
        /// <param name="BidCloseDate"></param>
        /// <returns></returns>
        Task<BiddingFilterNewsVM> BiddingFilter(DateTime? DateInserted, DateTime? BidStartDate, DateTime? BidCloseDate);
    }
}
