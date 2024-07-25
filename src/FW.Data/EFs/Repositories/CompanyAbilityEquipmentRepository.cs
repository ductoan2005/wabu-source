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
    public class CompanyAbilityEquipmentRepository : RepositoryBase<CompanyAbilityEquipment, long?>, ICompanyAbilityEquipmentRepository
    {
        public CompanyAbilityEquipmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IEnumerable<CompanyAbilityEquipment>> ReadCompanyAbilityFinanceHasPagingByUserId(IPaginationInfo iPaginationInfo, long? userId,
            List<SortOption> orderByStr = null)
        {
            IEnumerable<CompanyAbilityEquipment> resultList = new List<CompanyAbilityEquipment>();
            var companyId = await GetCompanyIdFromUserId(userId);
            resultList = dbSet.Where(c => c.IsDeleted != true && c.CompanyId == companyId)
                .OrderByDescending(c => c.DateInserted)
                .ThenByDescending(c => c.DateUpdated);
            iPaginationInfo.TotalItems = resultList.Count();

            return resultList.Skip(iPaginationInfo.ItemsToSkip)
                           .Take(iPaginationInfo.ItemsPerPage)
                           .AsEnumerable();
        }

        private async Task<long?> GetCompanyIdFromUserId(long? userId) => (await DbContext.Company.FirstOrDefaultAsync(c => c.UserId == userId && c.IsDeleted != true))?.Id;
    }
}