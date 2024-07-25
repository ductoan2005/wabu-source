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
    public class LoginHistoryRepository : RepositoryBase<LoginHistory, long?>, ILoginHistoryRepository
    {
        public LoginHistoryRepository(IDbFactory dbFactory)
          : base(dbFactory)
        {
        }

        public LoginHistory GetLoginHistory(long? id)
        {
            var loginHistoryList = new LoginHistory();

            loginHistoryList = this.DbContext.LoginHistories.FirstOrDefault(c => c.Id == id);

            return loginHistoryList;
        }
    }
}
