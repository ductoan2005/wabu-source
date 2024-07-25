using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    public interface IUserMasterBL
    {
        #region Create
        void CreateUser(UserMasterVM userMasterVM);
        #endregion

        #region Update
        Task<UserMasterVM> UpdateUser(UserMasterVM userMasterVM, bool isAdminUpdate = false);
        #endregion

        #region Read

        IEnumerable<UserMasterVM> ReadAllUsers();

        List<UserMasterVM> ReadUsersToPagingByCondition(IPaginationInfo paginationInfo, int selectedTab, string condition, string orderByStr = null);

        List<UserMasterVM> ReadUsersToPagingAdmin(IPaginationInfo paginationInfo, string condition, string orderByStr = null);

        List<UserMasterVM> ReadUsersToPagingInvestor(IPaginationInfo paginationInfo, string condition, string orderByStr = null);

        List<UserMasterVM> ReadUsersToPagingContractors(IPaginationInfo paginationInfo, string condition, string orderByStr = null);

        Task<UserMasterVM> ReadUserById(long? id);

        Task<CompanyVM> ReadCompanyByUserId(long? userId);

        #endregion

        #region Update
        void UpdateUserById(UserMasterVM userMasterVM);
        #endregion

        #region Delete
        void DeleteUserById(long? id);
        #endregion

    }
}
