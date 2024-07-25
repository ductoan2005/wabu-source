using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;

namespace FW.Data.EFs.Repositories
{
    public class RegisterRepository : RepositoryBase<Users, long?>, IRegisterRepository
    {
        public RegisterRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
