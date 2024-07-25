using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class CompanyStaffRepository : RepositoryBase<CompanyStaff, long?>, ICompanyStaffRepository
    {
        public CompanyStaffRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
