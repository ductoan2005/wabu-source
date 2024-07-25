using FW.Common.Pagination;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class UserMasterRepository : RepositoryBase<Users, long?>, IUserMasterRepository
    {
        public UserMasterRepository(IDbFactory dbFactory)
           : base(dbFactory)
        {
        }
        #region Create
        #endregion

        #region Read
        public IEnumerable<Users> ReadUsersToPagingByCondition(PaginationInfo paginationInfo, int selectedTab, string condition, string orderByStr)
        {
            var query = dbSet;
            var lstAuthority = new byte?[] { 0, 1 };

            IEnumerable<Users> resultList = null;
            if (selectedTab == 1) // get root, admin
            {
                resultList = query.Where(c => c.IsDeleted != true && lstAuthority.Contains(c.Authority)).AsQueryable();
            }
            else // get investor, contractor
            {
                resultList = query.Where(c => c.IsDeleted != true && c.Authority == selectedTab).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(condition))
            {
                resultList = resultList.Where(p => p.UserName.StartsWith(condition)
                                                || p.FullName.StartsWith(condition)
                                                || p.Email.StartsWith(condition));
            }

            resultList.OrderBy(orderByStr).ToList();
            paginationInfo.TotalItems = resultList.Count();

            return resultList.Skip(paginationInfo.ItemsToSkip)
                             .Take(paginationInfo.ItemsPerPage);
        }

        public IEnumerable<Users> ReadUsersToPagingAdmin(PaginationInfo paginationInfo, string condition, string orderByStr)
        {
            var lstAuthority = new byte?[] { 0, 1 };
            var query = dbSet;
            IEnumerable<Users> resultList = query.Where(c => c.IsDeleted != true && lstAuthority.Contains(c.Authority)
                                                    && (c.UserName.StartsWith(condition)
                                                     || c.FullName.StartsWith(condition)
                                                     || c.Email.StartsWith(condition))).OrderBy(orderByStr).ToList();
            paginationInfo.TotalItems = resultList.Count();
            return resultList.Skip(paginationInfo.ItemsToSkip)
                             .Take(paginationInfo.ItemsPerPage);
        }

        public IEnumerable<Users> ReadUsersToPagingInvestor(PaginationInfo paginationInfo, string condition, string orderByStr)
        {
            var query = dbSet;
            IEnumerable<Users> resultList = query.Where(c => c.IsDeleted != true && c.Authority == 2
                                                    && (c.UserName.StartsWith(condition)
                                                     || c.FullName.StartsWith(condition)
                                                     || c.Email.StartsWith(condition))).OrderBy(orderByStr).ToList();
            paginationInfo.TotalItems = resultList.Count();
            return resultList.Skip(paginationInfo.ItemsToSkip)
                             .Take(paginationInfo.ItemsPerPage);
        }

        public IEnumerable<Users> ReadUsersToPagingContractors(PaginationInfo paginationInfo, string condition, string orderByStr)
        {
            var query = dbSet;
            IEnumerable<Users> resultList = query.Where(c => c.IsDeleted != true && c.Authority == 3
                                                    && (c.UserName.StartsWith(condition)
                                                     || c.FullName.StartsWith(condition)
                                                     || c.Email.StartsWith(condition))).OrderBy(orderByStr).ToList();
            paginationInfo.TotalItems = resultList.Count();
            return resultList.Skip(paginationInfo.ItemsToSkip)
                             .Take(paginationInfo.ItemsPerPage);
        }

        /// <summary>
        /// ReadUserById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users ReadUserById(long? id)
        {
            var user = this.DbContext.Users.Where(c => c.Id == id && c.IsDeleted != true).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// ReadUserByUserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Users ReadUserByUserName(string userName, string email)
        {
            var user = dbSet.Where(c => (c.UserName == userName || c.Email== email) && c.IsDeleted != true).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// CheckUserInfoIsRegisCompleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckUserInfoIsRegisCompleted(long? userId)
            => dbSet.Any(x => x.Id == userId && x.FullName != string.Empty && x.Email != string.Empty && x.PhoneNumber != string.Empty && x.IsDeleted != true);

        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

    }
}
