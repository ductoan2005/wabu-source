using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingNewsBookmarkRepository : IRepository<BiddingNewsBookmark, long?>
    {
        List<BiddingNewsBookmark> ReadBiddingNewsBookmarkToPagingByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr);
    }
}
