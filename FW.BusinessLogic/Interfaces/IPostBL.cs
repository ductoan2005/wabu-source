﻿using FW.Common.Pagination.Interfaces;
using FW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    public interface IPostBL
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<IEnumerable<Post>> GetPostsByFilters(IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<Post> GetPostByIdAsync(int id);
    }
}
