using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System.Collections.Generic;

namespace FW.Data.RepositoryInterfaces
{
    public interface IUserMasterRepository : IRepository<Users, long?>
    {
        #region Create
        #endregion

        #region Read     

        IEnumerable<Users> ReadUsersToPagingByCondition(PaginationInfo paginationInfo, int selectedTab, string condition, string orderByStr);

        IEnumerable<Users> ReadUsersToPagingAdmin(PaginationInfo paginationInfo, string condition, string orderByStr);

        IEnumerable<Users> ReadUsersToPagingInvestor(PaginationInfo paginationInfo, string condition, string orderByStr);

        IEnumerable<Users> ReadUsersToPagingContractors(PaginationInfo paginationInfo, string condition, string orderByStr);

        Users ReadUserById(long? id);

        Users ReadUserByUserName(string userName, string email);

        bool CheckUserInfoIsRegisCompleted(long? userId);

        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
