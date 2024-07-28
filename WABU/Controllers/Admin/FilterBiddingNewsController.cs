using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_FILTER_BIDDINGNEWS, NeedReloadData = true)]
    public class FilterBiddingNewsController : BaseController
    {
        #region Property
        private readonly IBiddingNewsBL _biddingNewsBL;
        #endregion

        public FilterBiddingNewsController(IBiddingNewsBL biddingPackageBL)
        {
            this._biddingNewsBL = biddingPackageBL;
        }

        // GET: FilterBiddingNews
        public ActionResult Index()
        {
            ViewBag.pagingFilterBiddingResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "FilterBidding";
            return View(new BiddingNewsVM());
        }

        public ActionResult IndexInvalid()
        {
            ViewBag.pagingFilterBiddingResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "InvalidFilterBidding";
            return View(new BiddingNewsVM());
        }


        [HttpPost]
        public async Task<ActionResult> ReadBiddingFilterToPagingByCondition(bool isFilterValidNews, int page, string condition, List<SortOption> sortOption = null)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var paging = new PaginationInfo(page, int.MaxValue);
                var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
                var biddingNewsVm = await _biddingNewsBL.FilterBiddingNewsByCondition(isFilterValidNews, paging, userProfile, condition, queryOrderBy);

                ViewBag.pagingFilterBiddingResult = paging;
                ViewBag.filterBiddingNewsVmList = biddingNewsVm;

                var renderResult = isFilterValidNews ? "_FilterBiddingResult" : "_FilterInvalidBiddingResult";
                return Json(new { patialView = RenderPartialView(this, renderResult) });
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

        [HttpPost]
        public async Task<ActionResult> UpdateActiveStatus(long id, bool isActive)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                return Json(new { succeed = CommonConstants.STR_ZERO, isActive = await _biddingNewsBL.UpdateActiveStatus(id, isActive) });
            }
            catch (Exception ex)
            {
                ExportMsgExcaption(ex);
                return Json(new { succeed = CommonConstants.STR_ONE });
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteNews(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                await _biddingNewsBL.DeleteNews(id);
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                ExportMsgExcaption(ex);
                return Json(new { succeed = CommonConstants.STR_ONE });
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }
    }
}