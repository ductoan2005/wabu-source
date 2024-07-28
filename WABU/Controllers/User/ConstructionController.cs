using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers.User
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_CONSTRUCTION, NeedReloadData = true)]
    public class ConstructionController : BaseController
    {
        private readonly IConstructionBL _constructionBL;
        private readonly IAreaManageBL _areaManageBL;

        public ConstructionController(IConstructionBL constructionBL,
            IAreaManageBL areaManageBL)
        {
            _constructionBL = constructionBL;
            _areaManageBL = areaManageBL;
        }

        // GET: Construction
        public ActionResult Index()
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                if (SessionObjects.UserProfile == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var userProfile = SessionObjects.UserProfile;
                var pagingConstructionList = new PaginationInfo(1, 20);
                var constructionVmList = _constructionBL.GetAllConstructionHasPagingByUserId(pagingConstructionList, userProfile.UserID);
                ViewBag.constructionVmList = constructionVmList;
                ViewBag.pagingConstructionList = pagingConstructionList;
                ViewBag.selectListConstructionForm = SelectListItemControl.GetListConstructionFormForView();
                ViewBag.NameAccount = "listcontractor";
                return View();

            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_CONSTRUCTION,
                    string.Empty,
                    ex.ToString());
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetConstructionDetailById(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var constructionVM = await _constructionBL.GetConstructionToEditById(id);

                var listArea = _areaManageBL.GetAllArea();
                List<SelectListItem> listItems = new List<SelectListItem>(listArea);
                var itemSelected = listItems.FirstOrDefault(a => a.Value == constructionVM.AreaId.ToString());
                if (itemSelected != null)
                {
                    itemSelected.Selected = true;
                }

                ViewBag.ListArea = new SelectList(listArea, "Value", "Text", itemSelected.Value);
                ViewBag.selectListConstructionForm = SelectListItemControl.GetListConstructionFormForView();

                return PartialView("_PopUpEditConstruction", constructionVM);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_CONSTRUCTION,
                    string.Empty,
                    ex.ToString());
                throw;
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        public ActionResult GetPagingConstructionList(int page)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingConstructionList = new PaginationInfo(page, 20);
                var constructionVMList = _constructionBL.GetAllConstructionHasPagingByUserId(pagingConstructionList, userProfile.UserID);
                ViewBag.constructionVMList = constructionVMList;
                ViewBag.pagingConstructionList = pagingConstructionList;

                return Json(new { patialView = RenderPartialView(this, "_LstConstruction") });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_CONSTRUCTION,
                    string.Empty,
                    ex.ToString());
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewConstruction(ConstructionVM constructionVM)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                constructionVM.UserId = userProfile.UserID;
                if (string.IsNullOrEmpty(constructionVM.BuildingPermit))
                {
                    constructionVM.BuildingPermit = string.Empty;
                }

                if (ModelState.IsValid)
                {
                    await _constructionBL.AddNewConstruction(constructionVM);
                }
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_CONSTRUCTION,
                    string.Empty,
                    ex.ToString());
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateConstruction(ConstructionVM constructionVM)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                if (string.IsNullOrEmpty(constructionVM.BuildingPermit))
                {
                    constructionVM.BuildingPermit = string.Empty;
                }

                if (ModelState.IsValid)
                {
                    constructionVM.UserId = SessionObjects.UserProfile.UserID;
                    await _constructionBL.UpdateConstruction(constructionVM);
                }
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_CONSTRUCTION,
                    string.Empty,
                    ex.ToString());
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConstructionById(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                if (_constructionBL.CheckConstructionHasBidding(id))
                    return Json(new { succeed = CommonConstants.STR_ONE });
                await _constructionBL.DeleteConstructionById(id);
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_CONSTRUCTION,
                    string.Empty,
                    ex.ToString());
                throw;
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

    }
}