using FW.BusinessLogic.Interfaces;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Utilities;

namespace WABU.Controllers.User
{
    public class UserPostsController : BaseController
    {
        private readonly IPostBL _postBL;
        public UserPostsController(IPostBL postBL)
        {
            _postBL = postBL;
        }

        // GET: UserPosts
        public async Task<ActionResult> Index()
        {
            var listPost = await _postBL.GetPostsAsync();
            ViewBag.pagingFilterPostResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "Post";

            string json = "{Title:'',Username:'',IsEnable:true,CreatedDate:'',LastUpdatedDate:''}";
            int page = 1;

            var userProfile = SessionObjects.UserProfile;
            var paging = new PaginationInfo(page, int.MaxValue);
            var listPostVM = await _postBL.GetPostsByFilters(paging, userProfile, json);
            var listPostFiltered = listPostVM.Where(x => x.IsDeleted == true);
            ViewBag.pagingFilterPostResult = paging;
            ViewBag.filterPostVmList = listPostFiltered;
            return View(listPost);
        }

        [HttpPost]
        public async Task<ActionResult> GetPostsByFilters(int page, string condition, List<SortOption> sortOption = null)
        {
            ViewBag.pagingFilterPostResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "Post";

            try
            {
                var userProfile = SessionObjects.UserProfile;
                var paging = new PaginationInfo(page, int.MaxValue);
                var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
                var listPostVM = await _postBL.GetPostsByFilters(paging, userProfile, condition, queryOrderBy);
                var listPostFiltered = listPostVM.Where(x => x.IsDeleted == true);

                ViewBag.pagingFilterPostResult = paging;
                ViewBag.filterPostVmList = listPostFiltered;

                return Json(new { patialView = RenderPartialView(this, "_PostSearchResult"), listPostFiltered });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }

        // GET: UserPosts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await _postBL.GetPostByIdAsync(id);
            if (post == null || post.IsDeleted == false)
            {
                return HttpNotFound();
            }
            ViewBag.PostDetail = post;
            return View();
        }
    }
}
