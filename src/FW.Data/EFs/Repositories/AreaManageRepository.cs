using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;

namespace FW.Data.EFs.Repositories
{
    public class AreaManageRepository : RepositoryBase<Area, long?>, IAreaManageRepository
    {
        public AreaManageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
