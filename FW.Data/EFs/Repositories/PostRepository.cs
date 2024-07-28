using FW.Common.Pagination;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class PostRepository : RepositoryBase<Post, long?>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IEnumerable<Post>> FilterPostByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr)
        {
            IEnumerable<Post> result;
            IQueryable<Post> query;
            if (string.IsNullOrEmpty(condition))
            {
                query = dbSet.Where(x => x.IsDeleted != true);
                query = query.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);
                paginationInfo.TotalItems = query.Count();
                result = query.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();

                return await Task.FromResult(result);
            }
            var conditions = JsonConvert.DeserializeObject<PostVM>(condition);

            IQueryable<Post> query2 = dbSet;
            FilterPaging(conditions, ref query2);

            query = query2.OrderByDescending(x => x.DateInserted).ThenByDescending(x => x.DateUpdated);

            paginationInfo.TotalItems = query.Count();

            result = query.Skip(paginationInfo.ItemsToSkip)
                    .Take(paginationInfo.ItemsPerPage)
                    .ToList();
            return await Task.FromResult(result);
        }

        private static void FilterPaging(PostVM conditions, ref IQueryable<Post> query2)
        {
            string format = "dd/MM/yyyy";
            if (!string.IsNullOrWhiteSpace(conditions.Title))
            {
                query2 = query2.Where(x => x.Title.StartsWith(conditions.Title));
            }
            if (!string.IsNullOrWhiteSpace(conditions.Username))
            {
                query2 = query2.Where(x => x.Username.StartsWith(conditions.Username));
            }
            if (conditions.IsEnable != null)
            {
                query2 = query2.Where(x => x.IsDeleted == conditions.IsEnable);
            }
            if (!string.IsNullOrWhiteSpace(conditions.CreatedDate))
            {
                DateTime createdDate = DateTime.MinValue;
                DateTime.TryParseExact(conditions.CreatedDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out createdDate);
                query2 = query2.Where(x => x.DateInserted == createdDate);
            }
            if (!string.IsNullOrWhiteSpace(conditions.LastUpdatedDate))
            {
                DateTime lastUpdatedDate = DateTime.MinValue;
                DateTime.TryParseExact(conditions.LastUpdatedDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out lastUpdatedDate);
                query2 = query2.Where(x => x.DateUpdated == lastUpdatedDate);
            }
        }
    }
}
