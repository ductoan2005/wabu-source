using System.Collections.Generic;
using System.Threading.Tasks;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;

namespace FW.Data.RepositoryInterfaces
{
    public interface ICompanyAbilityFinanceRepository : IRepository<CompanyAbilityFinance, long?>
    {
        #region Read

        Task<IEnumerable<CompanyAbilityFinance>> ReadCompanyAbilityFinanceHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        #endregion
    }

}