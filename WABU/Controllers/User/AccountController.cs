using FW.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FW.BusinessLogic.Interfaces;
using FW.Common.Enum;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;
using Newtonsoft.Json;
using WABU.Filters;
using WABU.Utilities;
using System.Threading.Tasks;

namespace WABU.Controllers
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_ACCOUNT, NeedReloadData = true)]
    public class AccountController : BaseController
    {
        #region Variable

        private readonly IBiddingNewsBL _iBiddingNewsBL;
        private readonly IConstructionBL _constructionBL;
        private readonly IUserMasterBL _userMasterBL;
        private readonly ICompanyProfileBL _icompanyProfileBl;
        #endregion

        #region Constructor

        public AccountController(IConstructionBL constructionBl,
            IUserMasterBL userMasterBL,
            IBiddingNewsBL iBiddingNewsBL,
            ICompanyProfileBL icompanyProfileBl)
        {
            _constructionBL = constructionBl;
            _userMasterBL = userMasterBL;
            _iBiddingNewsBL = iBiddingNewsBL;
            _icompanyProfileBl = icompanyProfileBl;
        }

        #endregion

        #region Action

        public ActionResult PagePackage()
        {
            var methodName = SysLogger.GetMethodFullName();
            var viewModel = new BiddingNewsVM();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                if (userProfile == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var pagingPackageList = new PaginationInfo();
                var biddingNewsVmList = _iBiddingNewsBL.ReadBiddingNewsToPagingByCondition(pagingPackageList, userProfile, string.Empty);
                ViewBag.biddingNewsVmList = biddingNewsVmList;
                ViewBag.pagingPackageList = pagingPackageList;

                var selectListContruction = _constructionBL.GetSelectListConstruction(userProfile.UserID).ToList();
                selectListContruction.Insert(0, new SelectListItem
                {
                    Text = "Vui Lòng Chọn Công Trình",
                    Value = "0",
                    Selected = true
                });
                ViewBag.lstContructionName = selectListContruction;

                var selectListBiddingPackageName = SelectListItemControl.ListBiddingPackageNameForView();
                ViewBag.lstBiddingPackageName = selectListBiddingPackageName;

                ViewBag.Authority = userProfile.Authority;

                ViewBag.Nameaccount = "listpackage";

                viewModel.EnumStatusBidding = 0;
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
            return View("PagePackage", viewModel);
        }
        #endregion

        #region Page Contract

        [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_ACCOUNT_PAGECONTRACT, NeedReloadData = true)]
        //Page contract when choose from bidding news
        public ActionResult PageContract(long? biddingNewsId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                var userProfile = SessionObjects.UserProfile;
                if (SessionObjects.UserProfile != null)
                {
                    var pagingCompanyProfileList = new PaginationInfo();
                    ViewBag.lstContract = _icompanyProfileBl.ReadAllCompanyProfiles(userProfile, pagingCompanyProfileList, biddingNewsId);
                    ViewBag.pagingContract = pagingCompanyProfileList;
                    ViewBag.Nameaccount = "listcontract";
                }
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile?.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    string.Empty,
                    ex.ToString());
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
            return View();
        }

        #endregion

        [HttpPost]
        public ActionResult ReadPagingContractList(int page)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingCompanyProfileList = new PaginationInfo(page);
                ViewBag.lstContract = _icompanyProfileBl.ReadAllCompanyProfiles(userProfile, pagingCompanyProfileList);
                ViewBag.pagingContract = pagingCompanyProfileList;
                return Json(new { patialView = RenderPartialView(this, "_LstContract") });
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

        [HttpPost]
        public ActionResult ReadPagingPackageList(int page, string condition = null)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingPackageList = new PaginationInfo(page);
                var biddingNewsVmList = _iBiddingNewsBL.ReadBiddingNewsToPagingByCondition(pagingPackageList, userProfile, condition);
                ViewBag.biddingNewsVmList = biddingNewsVmList;
                ViewBag.pagingPackageList = pagingPackageList;
                ViewBag.Authority = userProfile.Authority;
                return Json(new { patialView = RenderPartialView(this, "_LstPackageDetail") });

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

        #region Account Information

        [HttpGet]
        public async Task<ActionResult> ProfileManagement()
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                long? userId = userProfile.UserID;
                var accountInfor = await _userMasterBL.ReadUserById(userId);
                ViewBag.CompanyInfo = await _userMasterBL.ReadCompanyByUserId(userId);
                ViewBag.Authority = userProfile.Authority;
                ViewBag.Nameaccount = "account";

                return View(accountInfor);
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

        [HttpPost]
        public async Task<ActionResult> UpdateInformation(UserMasterVM userVM)
        {
            try
            {
                if (SessionObjects.UserProfile == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                UserMasterVM userMasterVM = new UserMasterVM();
                if (ModelState.IsValid)
                {
                    userVM.Id = SessionObjects.UserProfile.UserID;
                    userMasterVM = await _userMasterBL.UpdateUser(userVM);
                }

                return Json(new { succeed = CommonConstants.STR_ZERO, data = userMasterVM });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }
        #endregion
    }
}