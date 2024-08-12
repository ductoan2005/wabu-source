using FW.BusinessLogic.Interfaces;
using FW.BusinessLogic.Services;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Common.Utilities;
using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class PostBL : BaseBL, IPostBL
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentsToDOServices _attachmentsToDOServices;

        internal const string ORDER_BY_DEFAULT = "DateUpdated";
        public PostBL(IPostRepository postRepository,
                    IUnitOfWork unitOfWork,
                    IAttachmentsToDOServices attachmentsToDOServices)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _attachmentsToDOServices = attachmentsToDOServices;
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

        public async Task<bool> CreatePostAsync(PostVM post)
        {
            IFormFile iFormFile = FileUtils.ConvertToIFormFile(post.ThumbnailImage);
            string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);

            if (post.IsActive == "on")
            {
                post.IsEnable = true;
            }
            else
            {
                post.IsEnable = false;
            }
            bool isValidModel = CleanCheckPost(post);
            if (!isValidModel) return false;
            var postCreate = new Post()
            {
                Content = post.Content,
                Title = post.Title,
                Username = post.Username,
                IsDeleted = post.IsEnable,
                ThumbnailImageFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName,
                DateInserted = DateTime.Now
            };
            _postRepository.Add(postCreate);
            // tra ve ket qua sau khi thao tac DB
            DbExecutionResult result = await _unitOfWork.CommitAsync();
            CheckDbExecutionResultAndThrowIfAny(result);
            return true;
        }

        public async Task<bool> UpdatePostAsync(PostVM post)
        {
            if (post.IsActive == "on")
            {
                post.IsEnable = true;
            }
            else
            {
                post.IsEnable = false;
            }
            bool isValidModel = CleanCheckPost(post);
            if (!isValidModel) return false;

            Post postDetail = await GetPostByIdAsync(post.Id);
            if (postDetail == null) return false;

            if (post.ThumbnailImage?.ContentLength > 0)
            {
                // xoa file o storage
                await _attachmentsToDOServices.DeleteAttachmentsToDO(postDetail.ThumbnailImageFilePath);
                // upload file moi vao storage
                IFormFile iFormFile = FileUtils.ConvertToIFormFile(post.ThumbnailImage);
                string fileUploadName = await _attachmentsToDOServices.UploadAttachmentsToDO(iFormFile);
                postDetail.ThumbnailImageFilePath = ConfigurationManager.AppSettings["AttachmentUrl"] + fileUploadName;
            }

            postDetail.Username = post.Username;
            postDetail.Content = post.Content;
            postDetail.Title = post.Title;
            postDetail.IsDeleted = post.IsEnable;
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

            // xoa file o storage
            await _attachmentsToDOServices.DeleteAttachmentsToDO(postDetail.ThumbnailImageFilePath);

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

        private bool CleanCheckPost(PostVM post)
        {
            if (post == null || string.IsNullOrEmpty(post.Username) || string.IsNullOrEmpty(post.Content) || string.IsNullOrEmpty(post.Title))
            {
                return false;
            }
            return true;
        }
    }
}
