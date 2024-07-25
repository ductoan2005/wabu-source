using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingDetailRepository : IRepository<BiddingDetail, long?>
    {
        bool CheckCompanyProfileHasBidding(long? biddingNewsId, long? companyProfileId);

        /// <summary>
        /// GetByBiddingNewsAndComapanyProfile
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        Task<BiddingDetail> GetByBiddingNewsAndComapanyProfile(long? biddingNewsId, long? companyProfileId);

        bool CheckInvestorIsSelectContractor(long? biddingNewsId);
        /// GetCompanyprofileByBiddingnewsId
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        Task<BiddingDetail> GetCompanyprofileByBiddingnewsId(long? biddingNewsId);

        Task<BiddingDetail> GetBiddingNewsDetail(long? BiddingNewsDetailId);
    }
}
