using System.Collections.Generic;
using System.Threading.Tasks;
using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using FW.ViewModels.EmailVM;

namespace FW.Data.RepositoryInterfaces
{
    public interface ICompanyProfileRepository : IRepository<CompanyProfile, long?>
    {
        IEnumerable<CompanyProfile> ReadAllCompanyProfilesHasPaging(UserProfile userProfile, PaginationInfo paginationInfo);

        /// <summary>
        /// Get all company profile has not bidding with biddingNewsId and userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        Task<IEnumerable<CompanyProfile>> GetAllCompanyProfilesHasNotBidding(long? userId, long? biddingNewsId);
        Task<EmailVM> GetUserRole3ByCompanyProfileId(long? companyProfileId,long? biddingNewsId);
    }
}
