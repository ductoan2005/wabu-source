using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Linq;

namespace FW.Data.EFs.Repositories
{
    public class BiddingNewsDetailRepository : RepositoryBase<BiddingDetail, long?>, IBiddingNewsDetailRepository
    {
        #region Ctor

        public BiddingNewsDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        #endregion

        #region Methods

        public bool CheckContructionByProfileId(long? profileId)
        {
            bool checkcontruction = dbSet.Any(x => x.CompanyProfileId == profileId && x.IsDeleted != true);

            return checkcontruction;
        }
        #endregion
    }
}
