using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface ILoginRepository : IRepository<Users, long?>
    {
        Users GetAccount(string userName);

        UserProfile GetInfoAccount(long id);
    }
}
