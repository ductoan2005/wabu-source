using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using FW.ViewModels.BiddingNewsRegistration;

namespace FW.Data.RepositoryInterfaces
{
    public interface IContractContruction : IRepository<BiddingNewsAbilityExpVM, long?>
    {
        #region Read

        IEnumerable<BiddingNewsAbilityExpVM> ReadBiddingNewsToPagingByCondition(PaginationInfo paginationInfo, long? userId, string condition, string orderByStr);

        #endregion
    }
}
