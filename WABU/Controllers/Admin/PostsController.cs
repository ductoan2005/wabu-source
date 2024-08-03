using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers.Admin
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_ADMIN_POST, NeedReloadData = true)]
    public class PostsController : BaseController
    {
        private readonly IPostBL _postBL;
        public PostsController(IPostBL postBL)
        {
            _postBL = postBL;
        }

        // GET: Posts
        public async Task<ActionResult> Index()
        {
            var listPost = await _postBL.GetPostsAsync();
            ViewBag.pagingFilterPostResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "Post";

            string json = "{Title:'',Username:'',IsEnable:false,CreatedDate:'',LastUpdatedDate:''}";
            int page = 1;

            var userProfile = SessionObjects.UserProfile;
            var paging = new PaginationInfo(page, int.MaxValue);
            var listPostVM = await _postBL.GetPostsByFilters(paging, userProfile, json);
            ViewBag.pagingFilterPostResult = paging;
            ViewBag.filterPostVmList = listPostVM;
            return View(listPost);
        }

        // POST: Posts by filters
        [HttpPost]
        public async Task<ActionResult> GetPostsByFilters(int page, string condition, List<SortOption> sortOption = null)
        {
            ViewBag.pagingFilterPostResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "Post";

            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var paging = new PaginationInfo(page, int.MaxValue);
                var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
                var listPostVM = await _postBL.GetPostsByFilters(paging, userProfile, condition, queryOrderBy);

                ViewBag.pagingFilterPostResult = paging;
                ViewBag.filterPostVmList = listPostVM;

                return Json(new { patialView = RenderPartialView(this, "_FilterPostResult") });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create(PostVM post)
        {
            try
            {
                bool result = await _postBL.CreatePostAsync(post);

                if (result)
                {
                    TempData["SuccessMessage"] = "Tạo mới bài viết thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Tạo bài viết thất bại, vui lòng liên hệ admin";
                }
                return RedirectToRoute("Quan Ly Bai Viet");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Xóa bài viết thất bại, vui lòng liên hệ admin" + ex.Message;
                ExportMsgExcaption(ex);
                return RedirectToRoute("Quan Ly Bai Viet");
            }
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await _postBL.GetPostByIdAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostDetail = post;
            return View();
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Username,Content,IsActive")] PostVM post)
        {
            try
            {
                bool result = await _postBL.UpdatePostAsync(post);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật bài viết thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Cập nhật bài viết thất bại, vui lòng liên hệ admin";
                }
                return RedirectToRoute("Quan Ly Bai Viet");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Xóa bài viết thất bại, vui lòng liên hệ admin" + ex.Message;
                ExportMsgExcaption(ex);
                return RedirectToRoute("Quan Ly Bai Viet");
            }
        }

        // DELETE: Posts/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool result = await _postBL.DeletePostAsync(id);

                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa bài viết thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Xóa bài viết thất bại, vui lòng liên hệ admin";
                }
                return RedirectToRoute("Quan Ly Bai Viet");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Xóa bài viết thất bại, vui lòng liên hệ admin" + ex.Message;
                ExportMsgExcaption(ex);
                return RedirectToRoute("Quan Ly Bai Viet");
            }
        }
    }
}
