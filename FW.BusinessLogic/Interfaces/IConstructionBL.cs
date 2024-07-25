using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.Models;
using FW.ViewModels;

namespace FW.BusinessLogic.Interfaces
{
    public interface IConstructionBL
    {
        #region Create

        Task AddNewConstruction(ConstructionVM contructionVm);

        #endregion

        #region Read

        IEnumerable<ConstructionVM> GetAllConstructionHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        Task<ConstructionVM> GetConstructionToEditById(long id);

        ConstructionVM GetConstructionDetailById(long idContraction);

        IEnumerable<SelectListItem> GetSelectListConstruction(long? userId);

        string GetImageConstructionById(long? id);

        Task<IEnumerable<ConstructionVM>> FilterConstructionByCondition(IPaginationInfo iPaginationInfo,
            UserProfile userProfile, string condition, string orderByStr = null);

        #endregion

        #region Update

        Task UpdateConstruction(ConstructionVM contructionVm);

        #endregion

        #region Delete

        Task DeleteConstructionById(long id);

        #endregion

        bool CheckConstructionHasBidding(long id);

    }
}
