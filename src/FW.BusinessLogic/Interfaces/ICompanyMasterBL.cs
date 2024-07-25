using FW.Common.Pagination.Interfaces;
using FW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    public interface ICompanyMasterBL
    {
        List<CompanyMasterVM> GetCompanies(IPaginationInfo paginationInfo, string orderByStr = null,
           string companyCode = null);

        Task<CompanyMasterVM> GetCompany(long? id);
        Task<CompanyMasterVM> GetCompanyByUserId(long? id);

        CompanyMasterVM GetCompanyByCode(string companyCode);

        void CreateCompany(CompanyMasterVM company);

        void UpdateCompany(CompanyMasterVM company);

        void DeleteCompany(long? id);

        IEnumerable<CompanyMasterVM> GetAll();
    }
}
