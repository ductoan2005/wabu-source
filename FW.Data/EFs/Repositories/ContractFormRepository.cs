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
    public class ContractFormRepository : RepositoryBase<ContractForm, long?>, IContractFormRepository
    {
        public ContractFormRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
