using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface IHomeRepository : IRepository<Construction, long?>
    {
        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsBest();
        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsNewest();
        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsInterest();
        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsSuggest();
        
    }
}
