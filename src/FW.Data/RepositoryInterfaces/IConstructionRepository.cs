using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System.Collections.Generic;
using FW.Common.Pagination;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface IConstructionRepository : IRepository<Construction, long?>
    {
        #region Create

        #endregion

        #region Read

        IEnumerable<Construction> GetAllConstructionByUserId(long? userId);

        IEnumerable<Construction> GetAllConstructionHasPagingByUserId(PaginationInfo paginationInfo, long? userId);

        Construction GetImageConstructionById(long? Id);

        Task<IEnumerable<Construction>> FilterConstructionByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr);

        #endregion

        #region Update


        #endregion

        #region Delete

        #endregion
    }
}
