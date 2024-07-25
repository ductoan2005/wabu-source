using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System.Collections.Generic;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingPackageRepository : IRepository<BiddingPackage, long?>
    {
        IEnumerable<BiddingPackage> ReadBiddingPackage();
       
    }
}
