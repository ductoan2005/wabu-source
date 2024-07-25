using System.Data.Entity;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class BiddingDetailRepository : RepositoryBase<BiddingDetail, long?>, IBiddingDetailRepository
    {
        public BiddingDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        //Check company profile has bidding
        public bool CheckCompanyProfileHasBidding(long? biddingNewsId, long? companyProfileId)
            => dbSet.Where(x => x.BiddingNewsId == biddingNewsId && x.IsDeleted != true).Any(x => x.CompanyProfileId == companyProfileId);

        /// <summary>
        /// GetByBiddingNewsAndComapanyProfile
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        public Task<BiddingDetail> GetByBiddingNewsAndComapanyProfile(long? biddingNewsId, long? companyProfileId)
            => dbSet.FirstOrDefaultAsync(x => x.BiddingNewsId == biddingNewsId && x.CompanyProfileId == companyProfileId && x.IsDeleted != true);

        /// <summary>
        /// CheckInvestorIsSelectContractor by bidding news
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        public bool CheckInvestorIsSelectContractor(long? biddingNewsId)
            => dbSet.Any(x => x.BiddingNewsId == biddingNewsId && x.InvestorSelected && x.IsDeleted != true);

        /// <summary>
        /// GetCompanyprofileByBiddingnewsId
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <returns></returns>
        public Task<BiddingDetail> GetCompanyprofileByBiddingnewsId(long? biddingNewsId)
        {
            return dbSet.FirstOrDefaultAsync(x => x.BiddingNewsId == biddingNewsId && x.InvestorSelected && x.IsDeleted != true);
        }

        public Task<BiddingDetail> GetBiddingNewsDetail(long? biddingNewsDetailId)
        {
            return dbSet.FirstOrDefaultAsync(x => x.Id == biddingNewsDetailId && x.IsDeleted != true);
        }
    }
}
