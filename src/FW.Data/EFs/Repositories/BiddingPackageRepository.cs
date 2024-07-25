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
    public class BiddingPackageRepository : RepositoryBase<BiddingPackage, long?>, IBiddingPackageRepository
    {
        public BiddingPackageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<BiddingPackage> ReadBiddingPackage()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BiddingPackage> GetCompanyReputation()
        {
            throw new NotImplementedException();
        }
    }
}
