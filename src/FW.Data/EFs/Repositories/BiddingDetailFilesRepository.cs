using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;

namespace FW.Data.EFs.Repositories
{
    public class BiddingDetailFilesRepository : RepositoryBase<BiddingDetailFiles, long?>, IBiddingDetailFilesRepository
    {
        public BiddingDetailFilesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
