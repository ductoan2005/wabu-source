using FW.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyBL
    {

        Task<string> GetCompanyNameFromUserId(long? userId);

        ContractorInformationVM GetContractorInformation(long userId);

        Task<long?> UpdateContractorInformation(CompanyVM viewModel);

        void UpdateContractFileUpload(ContractorInformationVM model);
        Task RatingStarForCompany(long companyProfileId, byte star);
    }
}
