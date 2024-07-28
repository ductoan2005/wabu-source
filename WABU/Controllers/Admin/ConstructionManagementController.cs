using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_FILTER_BIDDINGNEWS, NeedReloadData = true)]
    public class ConstructionManagementController : BaseController
    {
        #region Property
        private readonly IConstructionBL _constructionBL;
        #endregion

        #region Ctor
        public ConstructionManagementController(IConstructionBL constructionBL)
        {
            this._constructionBL = constructionBL;
        }
        #endregion

        #region Action
        // GET: FilterBiddingNews
        public ActionResult Index()
        {
            ViewBag.pagingFilterConstructionResult = new PaginationInfo(1, 20);
            ViewBag.activemenu = "ConstructionManagement";
            ViewBag.selectListConstructionForm = SelectListItemControl.GetListConstructionFormForView();
            return View(new ConstructionVM());
        }

        [HttpPost]
        public async Task<ActionResult> ReadConstructionPagingByCondition(int page, string condition, List<SortOption> sortOption = null)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var paging = new PaginationInfo(page, int.MaxValue);
                var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
                var biddingNewsVm = await _constructionBL.FilterConstructionByCondition(paging, userProfile, condition, queryOrderBy);

                ViewBag.pagingFilterConstructionResult = paging;
                ViewBag.constructionVmList = biddingNewsVm;

                return Json(new { patialView = RenderPartialView(this, "_FilterConstructionResult") });
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

        #endregion
    }
}