using FW.Common.Pagination;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface IPostRepository : IRepository<Post, long?>
    {
        Task<IEnumerable<Post>> FilterPostByCondition(PaginationInfo paginationInfo, UserProfile userProfile, string condition, string orderByStr);
    }
}
