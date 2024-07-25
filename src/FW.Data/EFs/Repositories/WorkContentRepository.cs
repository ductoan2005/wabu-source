using FW.Data.Infrastructure;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;

namespace FW.Data.EFs.Repositories
{
    public class WorkContentRepository : RepositoryBase<WorkContent, long?>, IWorkContentRepository
    {
        public WorkContentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<WorkContent> ReadAllWorkContentByBiddingPackage(long? Id)
        {
            var query = (from q in dbSet
                        join bp in (from b in DbContext.BiddingPackages
                                    where b.Id == Id
                                    select b)
                        on q.BiddingPackageId equals bp.Id

                        where q.IsDeleted == false
                        select q).ToList();

            return query;
        }
    }
}
