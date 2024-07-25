using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Linq;

namespace FW.Data.EFs.Repositories
{
    public class LoginRepository : RepositoryBase<Users, long?>, ILoginRepository
    {
        public LoginRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public Users GetAccount(string userName)
        {
            var account = this.DbContext.Users.FirstOrDefault(p => p.UserName == userName && p.IsDeleted == false && p.EmailConfirmed);

            return account;
        }

        public UserProfile GetInfoAccount(long id)
        {
            var account = this.DbContext.Users.Find(id);
            if (account.Authority == 2 || account.Authority == 1)
            {
                return new UserProfile
                {
                    UserID = account.Id,
                    UserName = account.UserName,
                    FullName = account.FullName,
                    Authority = account.Authority,
                    IsActive = account.IsActive,
                    AvatarPath = account.AvatarPath,
                    AvatarName = account.AvatarName
                };
            }

            var company = DbContext.Company.SingleOrDefault(c => c.UserId == id);

            return new UserProfile
            {
                UserID = account.Id,
                UserName = account.UserName,
                FullName = account.FullName,
                Authority = account.Authority,
                IsActive = account.IsActive,
                AvatarPath = company == null ? string.Empty : company.Logo,
            };
        }
    }
}
