using System;
using System.Collections.Generic;
using System.Data.Entity;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class BiddingNewsAbilityHRRepository : RepositoryBase<BiddingNewsAbilityHR, long?>, IBiddingNewsAbilityHRRepository
    {
        public BiddingNewsAbilityHRRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        /// <summary>
        /// GetJobPositionKeyWord
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetJobPositionKeyWord(string term)
             => Task.FromResult(dbSet.Where(x => x.JobPosition.ToLower().Contains(term.ToLower())).Select(x => x.JobPosition).AsNoTracking().AsEnumerable());
    }
}
