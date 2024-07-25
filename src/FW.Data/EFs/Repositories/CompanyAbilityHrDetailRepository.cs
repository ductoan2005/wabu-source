using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;

namespace FW.Data.EFs.Repositories
{
    public class CompanyAbilityHrDetailRepository : RepositoryBase<CompanyAbilityHRDetail, long?>, ICompanyAbilityHrDetailRepository
    {
        public CompanyAbilityHrDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
