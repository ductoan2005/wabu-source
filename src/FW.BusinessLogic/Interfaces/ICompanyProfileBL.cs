using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Pagination.Interfaces;
using FW.Models;
using FW.ViewModels;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyProfileBL
    {
        List<CompanyProfileVM> ReadAllCompanyProfiles(UserProfile userProfile, IPaginationInfo iPaginationInfo, long? biddingNewsId = null);

        Task<JsonResult> AddNewCompanyProfile(CompanyProfileVM companyProfileVM, long? userId);

        IEnumerable<Tuple<long?, bool>> CheckAbilityProfileExisted(List<long?> listId, string typeOfAbility);

        Task<CompanyProfileVM> GetCompanyProfileById(long? id);

        Task<CompanyAbilityExpVM> GetCompanyAbilityExpDetailById(long? id);

        Task<CompanyAbilityEquipmentVM> GetCompanyAbilityEquipmentDetailById(long? id);

        Task<CompanyAbilityHRVM> GetCompanyAbilityExpHrDetailById(long? id);

        Task<CompanyAbilityFinanceVM> GetCompanyAbilityFinanceDetailById(long? id);

        List<SelectListItem> CreateAbilityEquipmentSourceListItem();
    }
}
