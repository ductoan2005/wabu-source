using FW.BusinessLogic.Interfaces;
using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers.User
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_BIDDING_NEWS_BOOKMARK, NeedReloadData = true)]
    public class BiddingNewsBookmarkController : BaseController
    {
        #region Property

        private readonly IBiddingNewsBookmarkBL _biddingNewsBookmarkBL;

        #endregion

        public BiddingNewsBookmarkController(IBiddingNewsBookmarkBL biddingNewsBookmarkBL)
        {
            _biddingNewsBookmarkBL = biddingNewsBookmarkBL;
        }

        // GET: BiddingNewsBookmark
        [HttpGet]
        public ActionResult Index()
        {
            var methodName = SysLogger.GetMethodFullName();
            var viewModel = new BiddingNewsBookmarkVM();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                if (userProfile == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var pagingBiddingNewsBookmarkList = new PaginationInfo();
                var biddingNewsBookmarkList = _biddingNewsBookmarkBL.ReadBiddingNewsBookmarkToPagingByCondition(pagingBiddingNewsBookmarkList, userProfile, string.Empty);
                ViewBag.biddingNewsBookmarkList = biddingNewsBookmarkList;
                ViewBag.pagingBiddingNewsBookmarkList = pagingBiddingNewsBookmarkList;

                //Get list contruction
                var selectListContruction = _biddingNewsBookmarkBL.GetSelectListConstruction();
                selectListContruction.Insert(0, new SelectListItem
                {
                    Text = "Vui Lòng Chọn Công Trình",
                    Value = "0",
                    Selected = true
                });
                ViewBag.lstContructionName = selectListContruction;

                var selectListBiddingPackageName = SelectListItemControl.ListBiddingPackageNameForView();
                ViewBag.lstBiddingPackageName = selectListBiddingPackageName;

                viewModel.StatusBiddingNewsBookmark = 0;
                ViewBag.Nameaccount = "listpackageintereste";
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    string.Empty,
                    string.Empty,
                    ex.ToString());
                throw;
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ReadPagingBiddingNewsBookmarkList(int page, string condition = null)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingBiddingNewsBookmarkList = new PaginationInfo(page);
                var biddingNewsBookmarkList = _biddingNewsBookmarkBL.ReadBiddingNewsBookmarkToPagingByCondition(pagingBiddingNewsBookmarkList, userProfile, condition);

                ViewBag.biddingNewsBookmarkList = biddingNewsBookmarkList;
                ViewBag.pagingBiddingNewsBookmarkList = pagingBiddingNewsBookmarkList;
                ViewBag.Authority = userProfile.Authority;
                return Json(new { patialView = RenderPartialView(this, "_LstBiddingBookmarkDetail") });

            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    string.Empty,
                    string.Empty,
                    ex.ToString());
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }
    }
}