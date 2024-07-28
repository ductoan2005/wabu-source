using FW.BusinessLogic.Interfaces;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class PostBL : BaseBL, IPostBL
    {
        private readonly IPostRepository _postRepository;

        internal const string ORDER_BY_DEFAULT = "DateUpdated";
        public PostBL(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            IEnumerable<Post> listPosts = await _postRepository.GetAllAsync();
            return listPosts;
        }

        public async Task<IEnumerable<Post>> GetPostsByFilters(IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }
            IEnumerable<Post> listPosts = await _postRepository.FilterPostByCondition(paginationInfo, userProfile, condition, orderByStr);

            return listPosts;
        }
    }
}
