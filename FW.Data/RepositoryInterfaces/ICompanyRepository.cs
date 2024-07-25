using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using FW.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using FW.ViewModels.Home;

namespace FW.Data.RepositoryInterfaces
{
    public interface ICompanyRepository: IRepository<Company, long?>
    {
        IEnumerable<CompanyRatingVM> GetCompanyRating();

        IEnumerable<CompanyProfileLogoVM> ReadCompayLogoOnOver();

        Task<Company> ReadCompanyFromUserId(long? userId);

        Task<string> GetCompanyNameFromUserId(long? userId);

    } 
}
