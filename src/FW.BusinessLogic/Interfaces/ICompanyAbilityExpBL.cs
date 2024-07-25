using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyAbilityExpBL
    {
        Task<JsonResult> AddNewCompanyAbilityExp(CompanyAbilityExpVM companyAbilityExpVM, long? userId);

        Task<IEnumerable<CompanyAbilityExpVM>> ReadCompanyAbilityExpHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        IEnumerable<CompanyAbilityExpVM> ReadAllListCompanyAbilityExpBy(long? Id);

        Task<CompanyAbilityExpVM> ReadAllCompanyAbilityExpBy(long? Id);

        Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityExp(List<long?> listId, string typeOfAbility);
    }
}