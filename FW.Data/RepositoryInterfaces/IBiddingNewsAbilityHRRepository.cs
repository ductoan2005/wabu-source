using System.Collections.Generic;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingNewsAbilityHRRepository : IRepository<BiddingNewsAbilityHR, long?>
    {
        /// <summary>
        /// GetJobPositionKeyWord
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetJobPositionKeyWord(string term);
    }
}
