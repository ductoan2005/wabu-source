using System.Collections.Generic;
using System.Threading.Tasks;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;

namespace FW.Data.RepositoryInterfaces
{
    public interface ICompanyAbilityExpRepository : IRepository<CompanyAbilityExp, long?>
    {
        #region Read

        Task<IEnumerable<CompanyAbilityExp>> ReadCompanyAbilityExpHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        IEnumerable<CompanyAbilityExp> ReadAllCompanyAbilityExpBy(long? Id);

        #endregion
    }
}