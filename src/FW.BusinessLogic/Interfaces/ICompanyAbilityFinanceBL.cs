using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyAbilityFinanceBL
    {
        Task<JsonResult> AddNewCompanyAbilityFinance(CompanyAbilityFinanceVM companyAbilityFinanceVM, long? userId);

        Task<JsonResult> UpdateCompanyAbilityFinance(CompanyAbilityFinanceVM companyAbilityFinanceVM, long? userId);

        Task<IEnumerable<CompanyAbilityFinanceVM>> ReadCompanyAbilityFinanceHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityFinance(List<long?> listId, string typeOfAbility);

        Task<CompanyAbilityFinanceVM> ReadAllCompanyAbilityFinanceBy(long? Id);
    }
}