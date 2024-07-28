using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Objects;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using WABU.Utilities;

namespace WABU.Controllers
{
    public class UserMasterController : BaseController
    {
        #region Constructor
        private readonly IUserMasterBL _userMasterBL;

        public UserMasterController(IUserMasterBL userMasterBL)
        {
            this._userMasterBL = userMasterBL;
        }
        #endregion

        #region Action
        // GET: UserMaster
        public async Task<ActionResult> Index()
        {
            var userProfile = SessionObjects.UserProfile;
            if (userProfile == null)
            {
                return RedirectToAction("Index", "Login");

            }

            ViewBag.activemenu = "UserMaster";

            #region List tab user
            var pagingAdmin = new PaginationInfo();
            var userAdminVMs = _userMasterBL.ReadUsersToPagingAdmin(pagingAdmin, string.Empty);
            ViewBag.listUserAdmin = userAdminVMs;
            ViewBag.pagingUserAdmin = pagingAdmin;

            var pagingInvestor = new PaginationInfo();
            var userInvestorVMs = _userMasterBL.ReadUsersToPagingInvestor(pagingInvestor, string.Empty);
            ViewBag.listUserInvestor = userInvestorVMs;
            ViewBag.pagingUserInvestor = pagingInvestor;

            var pagingContractors = new PaginationInfo();
            var userContractorsVMs = _userMasterBL.ReadUsersToPagingContractors(pagingContractors, string.Empty);
            ViewBag.listUserContractors = userContractorsVMs;
            ViewBag.pagingUserContractors = pagingContractors;
            #endregion

            #region Detail User

            ViewBag.userDetail = await _userMasterBL.ReadUserById(userAdminVMs.First().Id);

            if (ViewBag.userDetail.Authority == 3)
            {
                ViewBag.CompanyInfo = await _userMasterBL.ReadCompanyByUserId(ViewBag.userDetail.Id);
            }

            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult Create(UserMasterVM userVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _userMasterBL.CreateUser(userVM);
                }

                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }

        public ActionResult ReadAllUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DisplayAccountDetail(long? userId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userMasterVM = await _userMasterBL.ReadUserById(userId);
                ViewBag.userDetail = userMasterVM;

                if (userMasterVM.Authority == 3)
                {
                    ViewBag.CompanyInfo = await _userMasterBL.ReadCompanyByUserId(userId);
                }

                return Json(new { succeed = CommonConstants.STR_ZERO, patialView = RenderPartialView(this, "_DetailUser") });
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

        public ActionResult ReadUsersToPagingByCondition(int page, int selectedTab, string condition, List<SortOption> sortOption = null)
        {
            var paging = new PaginationInfo(page);
            var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
            var userVMs = _userMasterBL.ReadUsersToPagingByCondition(paging, selectedTab, condition, queryOrderBy);
            if (userVMs.Count < 1) //trường hợp page > 1 không có item
            {
                paging = new PaginationInfo(1);
                userVMs = _userMasterBL.ReadUsersToPagingByCondition(paging, selectedTab, condition, queryOrderBy);
            }

            string patialName = string.Empty;
            switch (selectedTab)
            {
                case 1:
                    ViewBag.listUserAdmin = userVMs;
                    ViewBag.pagingUserAdmin = paging;
                    patialName = "_LstTabAdmin";
                    break;
                case 2:
                    ViewBag.listUserInvestor = userVMs;
                    ViewBag.pagingUserInvestor = paging;
                    patialName = "_LstTabInvestor";
                    break;
                case 3:
                    ViewBag.listUserContractors = userVMs;
                    ViewBag.pagingUserContractors = paging;
                    patialName = "_LstTabContractors";
                    break;
                default:
                    break;
            }

            return Json(new { patialView = RenderPartialView(this, patialName) });
        }
        public ActionResult ReadUsersToPagingAdmin(int page, string condition, List<SortOption> sortOption = null)
        {
            var paging = new PaginationInfo(page);
            var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
            var userVMs = _userMasterBL.ReadUsersToPagingAdmin(paging, condition, queryOrderBy);
            if (userVMs.Count < 1) //trường hợp page > 1 không có item
            {
                paging = new PaginationInfo(1);
                userVMs = _userMasterBL.ReadUsersToPagingAdmin(paging, condition, queryOrderBy);
            }

            ViewBag.listUserAdmin = userVMs;
            ViewBag.pagingUserAdmin = paging;

            return Json(new { patialView = RenderPartialView(this, "_LstTabAdmin") });
        }

        public ActionResult ReadUsersToPagingInvestor(int page, string condition, List<SortOption> sortOption = null)
        {
            var paging = new PaginationInfo(page);
            var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
            var userVMs = _userMasterBL.ReadUsersToPagingInvestor(paging, condition, queryOrderBy);
            if (userVMs.Count < 1) //trường hợp page > 1 không có item
            {
                paging = new PaginationInfo(1);
                userVMs = _userMasterBL.ReadUsersToPagingInvestor(paging, condition, queryOrderBy);
            }

            ViewBag.listUserInvestor = userVMs;
            ViewBag.pagingUserInvestor = paging;

            return Json(new { patialView = RenderPartialView(this, "_LstTabInvestor") });
        }

        public ActionResult ReadUsersToPagingContractors(int page, string condition, List<SortOption> sortOption = null)
        {
            var paging = new PaginationInfo(page);
            var queryOrderBy = StringUtils.GenerateQuerySort(sortOption); // tạo query Order by
            var userVMs = _userMasterBL.ReadUsersToPagingContractors(paging, condition, queryOrderBy);
            if (userVMs.Count < 1) //trường hợp page > 1 không có item
            {
                paging = new PaginationInfo(1);
                userVMs = _userMasterBL.ReadUsersToPagingContractors(paging, condition, queryOrderBy);
            }

            ViewBag.listUserContractors = userVMs;
            ViewBag.pagingUserContractors = paging;

            return Json(new { patialView = RenderPartialView(this, "_LstTabContractors") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserMasterVM userMasterVM)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                if (ModelState.IsValid)
                {
                    bool isAdminUpdate = true;
                    await _userMasterBL.UpdateUser(userMasterVM, isAdminUpdate);
                }
                return Json(new { succeed = CommonConstants.STR_ZERO, authority = userMasterVM.Authority});
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

        public ActionResult Delete(long? id)
        {
            try
            {
                _userMasterBL.DeleteUserById(id);
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DisplayEditUserPopup(long? userId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userMasterVM = await _userMasterBL.ReadUserById(userId);

                if(userMasterVM.Authority == 3)
                {
                    ViewBag.CompanyInfo = await _userMasterBL.ReadCompanyByUserId(userId);
                }

                return Json(new { patialView = RenderPartialView(this, "_PopUpEditUser", userMasterVM) });
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

        #endregion

    }
}