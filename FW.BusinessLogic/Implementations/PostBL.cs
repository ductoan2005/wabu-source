﻿using FW.BusinessLogic.Interfaces;
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

        public async Task<bool> UpdatePostAsync(Post post)
        {
            bool isValidModel = CleanCheckPost(post);
            if (!isValidModel) return false;

            Post postDetail = await GetPostByIdAsync(post.Id);
            if (postDetail == null) return false;

            postDetail.Username = post.Username;
            postDetail.Content = post.Content;
            postDetail.Title = post.Title;
            postDetail.IsDeleted = post.IsDeleted;
            postDetail.DateUpdated = DateTime.Now;
            _postRepository.Update(postDetail);
            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _unitOfWork.CommitAsync();
            CheckDbExecutionResultAndThrowIfAny(result);
            return true;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            Post postDetail = await GetPostByIdAsync(id);
            if (postDetail == null) return false;

            _postRepository.Delete(postDetail);
            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _unitOfWork.CommitAsync();
            CheckDbExecutionResultAndThrowIfAny(result);
            return true;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;
            return post;
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
