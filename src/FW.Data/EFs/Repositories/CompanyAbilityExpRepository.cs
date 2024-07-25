using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FW.Common.Objects;
using FW.Common.Pagination.Interfaces;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;

namespace FW.Data.EFs.Repositories
{
    public class CompanyAbilityExpRepository : RepositoryBase<CompanyAbilityExp, long?>, ICompanyAbilityExpRepository
    {
        public CompanyAbilityExpRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IEnumerable<CompanyAbilityExp>> ReadCompanyAbilityExpHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId,
            List<SortOption> orderByStr = null)
        {
            IEnumerable<CompanyAbilityExp> resultList = new List<CompanyAbilityExp>();
            var companyId = await GetCompanyIdFromUserId(userId);
            resultList = dbSet.Where(c => c.IsDeleted != true && c.CompanyId == companyId)
                .OrderByDescending(c => c.DateInserted)
                .ThenByDescending(c => c.DateUpdated);
            iPaginationInfo.TotalItems = resultList.Count();
            return resultList.Skip(iPaginationInfo.ItemsToSkip)
                            .Take(iPaginationInfo.ItemsPerPage)
                            .AsEnumerable();
        }
        public IEnumerable<CompanyAbilityExp> ReadAllCompanyAbilityExpBy(long? Id)
        {
            var query = dbSet.Where(x=>x.Id== Id && x.IsDeleted == false).AsEnumerable();

            return query;
        }

        private async Task<long?> GetCompanyIdFromUserId(long? userId) => (await DbContext.Company.FirstOrDefaultAsync(c => c.UserId == userId && c.IsDeleted != true))?.Id;
    }
}