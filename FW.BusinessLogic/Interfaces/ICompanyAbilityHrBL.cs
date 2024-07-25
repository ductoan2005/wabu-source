using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyAbilityHrBL
    {
        Task<JsonResult> AddNewCompanyAbilityHr(CompanyAbilityHRVM companyAbilityHRVM, long? userId);

        Task<IEnumerable<CompanyAbilityHRVM>> ReadCompanyAbilityHrHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityHr(List<long?> listId, string typeOfAbility);

        Task<CompanyAbilityHRVM> ReadAllCompanyAbilityHrBy(long? Id);
    }
}