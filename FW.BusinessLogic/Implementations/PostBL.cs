using FW.BusinessLogic.Interfaces;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class PostBL : BaseBL, IPostBL
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        internal const string ORDER_BY_DEFAULT = "DateUpdated";
        public PostBL(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<bool> CreatePostAsync(Post post)
        {
            bool isValidModel = CleanCheckPost(post);
            if (!isValidModel) return false;
            var postCreate = new Post()
            {
                Content = post.Content,
                Title = post.Title,
                Username = post.Username,
                IsDeleted = post.IsDeleted,
                DateInserted = DateTime.Now
            };
            _postRepository.Add(postCreate);
            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _unitOfWork.CommitAsync();
            CheckDbExecutionResultAndThrowIfAny(result);
            return true;
        }

        public Task<bool> UpdatePostAsync(Post post)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeletePostAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        private bool CleanCheckPost(Post post)
        {
            if (post == null || string.IsNullOrEmpty(post.Username) || string.IsNullOrEmpty(post.Content) || string.IsNullOrEmpty(post.Title))
            {
                return false;
            }
            return true;
        }
    }
}
