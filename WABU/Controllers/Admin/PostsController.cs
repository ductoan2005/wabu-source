using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Data.EFs;
using FW.Models;
using FW.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers.Admin
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_FILTER_POST, NeedReloadData = true)]
    public class PostsController : BaseController
    {
        private readonly IPostBL _postBL;
        public PostsController(IPostBL postBL)
        {
            _postBL = postBL;
        }
        private readonly MyFWDbContext db = new MyFWDbContext();

        // GET: Posts
        public async Task<ActionResult> Index()
        {
            var listPost = await _postBL.GetPostsAsync();
            ViewBag.pagingFilterPostResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "Post";

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

        // GET: Posts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult DirectToPartialCreateView()
        {
            return View("_PopUpCreatePost");
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post)
        {
            try
            {
                bool result = await _postBL.CreatePostAsync(post);

                if (result)
                {
                    return Json(new { succeed = CommonConstants.STR_ZERO });
                }
                return Json(new { error = CommonConstants.STR_MINUS_ONE });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Username,Content,IsDeleted,DateInserted,DateUpdated")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Post.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Post.FindAsync(id);
            db.Post.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
