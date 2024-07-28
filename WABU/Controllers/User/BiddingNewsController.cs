using FW.BusinessLogic.Interfaces;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Utilities;

namespace WABU.Controllers
{
    public class BiddingNewsController : BaseController
    {
        #region Fields

        private readonly IHomeBL _homeBL;
        private readonly IBiddingNewsBL _biddingNewsBL;
        private readonly IBiddingNewsAbilityHRBL _biddingNewsAbilityHRBL;

        #endregion

        #region Ctor

        public BiddingNewsController(IHomeBL homeBL, IBiddingNewsBL biddingNewsBL, IBiddingNewsAbilityHRBL biddingNewsAbilityHRBL)
        {
            _homeBL = homeBL;
            _biddingNewsBL = biddingNewsBL;
            _biddingNewsAbilityHRBL = biddingNewsAbilityHRBL;
        }

        #endregion

        #region Methods

        // GET: BiddingNews
        public ActionResult Search()
        {
            var paging = new PaginationInfo(1, 20);
            ViewBag.pagingBiddingResult = paging;
            ViewBag.userInfo = SessionObjects.UserProfile;
            var viewModel = new BiddingNewsVM();

            return View(viewModel);
        }

        /// <summary>
        /// ReadBiddingNewsToPagingByCondition
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <param name="sortOption"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ReadBiddingNewsToPagingByCondition(int page, string condition, List<SortOption> sortOption = null)
        {
            var methodName = SysLogger.GetMethodFullName();

            try
            {
                var userProfile = SessionObjects.UserProfile;
                var paging = new PaginationInfo(page, 20);
                var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
                var biddingNewsVms = await _biddingNewsBL.SearchBiddingNewsByCondition(paging, userProfile, condition, queryOrderBy);

                ViewBag.pagingBiddingResult = paging;
                ViewBag.biddingNewsVmList = biddingNewsVms;

                return Json(new { patialView = RenderPartialView(this, "_BiddingSearchResult", biddingNewsVms) });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_BIDDING_NEWS,
                    string.Empty,
                    ex.ToString());
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// GetJobPositionKeyWord
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetJobPositionKeyWord(string keyword)
        {
            var methodName = SysLogger.GetMethodFullName();

            try
            {
                var filteredItems = await _biddingNewsAbilityHRBL.GetJobPositionKeyWord(keyword);

                return Json(filteredItems, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_BIDDING_NEWS,
                    string.Empty,
                    ex.ToString());
                throw;
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        #endregion
    }
}