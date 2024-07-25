using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyAbilityEquipmentBL
    {
        Task<JsonResult> AddNewCompanyAbilityEquipment(CompanyAbilityEquipmentVM companyAbilityEquipmentVM, long? userId);

        Task<IEnumerable<CompanyAbilityEquipmentVM>> ReadCompanyAbilityEquipmentHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId, List<SortOption> orderByStr = null);

        Task<List<ResponseDeleteAbilityProfileVM>> DeleteCompanyAbilityEquipment(List<long?> listId, string typeOfAbility);

        Task<CompanyAbilityEquipmentVM> ReadAllCompanyAbilityEquipmentBy(long? Id);

        List<SelectListItem> CreateAbilityEquipmentSourceListItem();

    }
}