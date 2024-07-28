using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Pagination;
using FW.Common.Utilities;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using FW.ViewModels.PageContractBid;
using WABU.Filters;
using WABU.Utilities;

namespace WABU.Controllers.User
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_PAGECONTRACTBID, NeedReloadData = true)]
    public class PageContractBidController : BaseController
    {
        #region Variable

        private readonly ICompanyAbilityExpBL _iCompanyAbilityExpBL;
        private readonly ICompanyAbilityFinanceBL _iCompanyAbilityFinanceBL;
        private readonly ICompanyAbilityHrBL _iCompanyAbilityHrBL;
        private readonly ICompanyAbilityEquipmentBL _iCompanyAbilityEquipmentBL;
        private readonly ICompanyProfileBL _companyProfileBL;

        #endregion

        #region Constructor

        public PageContractBidController(ICompanyAbilityExpBL iCompanyAbilityExpBL, ICompanyAbilityFinanceBL iCompanyAbilityFinanceBL,
            ICompanyAbilityHrBL iCompanyAbilityHrBL, ICompanyAbilityEquipmentBL iCompanyAbilityEquipmentBL, ICompanyProfileBL companyProfileBL)
        {
            _iCompanyAbilityExpBL = iCompanyAbilityExpBL;
            _iCompanyAbilityFinanceBL = iCompanyAbilityFinanceBL;
            _iCompanyAbilityHrBL = iCompanyAbilityHrBL;
            _iCompanyAbilityEquipmentBL = iCompanyAbilityEquipmentBL;
            _companyProfileBL = companyProfileBL;
        }

        #endregion

        #region Action

        // GET: PageContractBid
        public ActionResult Index()
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var pagingContractNLNSList = new PaginationInfo();
                ViewBag.pagingContractNLNSList = pagingContractNLNSList;
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
            return View();
        }

        /// <summary>
        /// AddNewCompanyAbilityExp
        /// </summary>
        /// <param name="companyAbilityExpVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewCompanyAbilityExp(CompanyAbilityExpVM companyAbilityExpVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(await _iCompanyAbilityExpBL.AddNewCompanyAbilityExp(companyAbilityExpVm, userProfile.UserID), JsonRequestBehavior.AllowGet);
                }

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEXP,
                    string.Empty,
                    ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// AddNewCompanyAbilityFinance
        /// </summary>
        /// <param name="companyAbilityFinanceVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewCompanyAbilityFinance(CompanyAbilityFinanceVM companyAbilityFinanceVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(await _iCompanyAbilityFinanceBL.AddNewCompanyAbilityFinance(companyAbilityFinanceVm, userProfile.UserID), JsonRequestBehavior.AllowGet);
                }

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYFINANCE,
                    string.Empty,
                    ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// UpdateCompanyAbilityFinance
        /// </summary>
        /// <param name="companyAbilityFinanceVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCompanyAbilityFinance(CompanyAbilityFinanceVM companyAbilityFinanceVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(await _iCompanyAbilityFinanceBL.UpdateCompanyAbilityFinance(companyAbilityFinanceVm, userProfile.UserID), JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    code = CommonConstants.STR_ONE,
                    message = CommonConstants.ADD_ERROR,
                    userId = userProfile.UserID
                });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYFINANCE,
                    string.Empty,
                    ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// AddNewCompanyAbilityHr
        /// </summary>
        /// <param name="companyAbilityHrVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewCompanyAbilityHr(CompanyAbilityHRVM companyAbilityHrVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                return Json(await _iCompanyAbilityHrBL.AddNewCompanyAbilityHr(companyAbilityHrVm, userProfile.UserID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYHR,
                    string.Empty,
                    ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// AddNewCompanyAbilityEquipment
        /// </summary>
        /// <param name="companyAbilityEquipmentVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewCompanyAbilityEquipment(CompanyAbilityEquipmentVM companyAbilityEquipmentVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                return Json(await _iCompanyAbilityEquipmentBL.AddNewCompanyAbilityEquipment(companyAbilityEquipmentVm, userProfile.UserID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEQUIPMENT,
                    string.Empty,
                    ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// ReadPagingCompanyAbilityExp
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ReadPagingCompanyAbilityExp(int page, string condition)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingCompanyAbilityExpList = new PaginationInfo(page);
                var companyAbilityExpList = await _iCompanyAbilityExpBL.ReadCompanyAbilityExpHasPagingByUserId(pagingCompanyAbilityExpList,
                        userProfile.UserID);
                ViewBag.companyAbilityExpList = companyAbilityExpList;
                ViewBag.pagingCompanyAbilityExpList = pagingCompanyAbilityExpList;
                return Json(new { patialView = RenderPartialView(this, "_ListNLKN"), partialModalView = RenderPartialView(this, "_Modal_ListNLKN") });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEXP,
                    string.Empty,
                    ex.ToString());
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// ReadPagingCompanyAbilityEquipment
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ReadPagingCompanyAbilityEquipment(int page, string condition)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingCompanyAbilityEquipmentList = new PaginationInfo(page);
                var companyAbilityEquipmentList = await _iCompanyAbilityEquipmentBL.ReadCompanyAbilityEquipmentHasPagingByUserId(pagingCompanyAbilityEquipmentList,
                        userProfile.UserID);
                ViewBag.companyAbilityEquipmentList = companyAbilityEquipmentList;
                ViewBag.pagingCompanyAbilityEquipmentList = pagingCompanyAbilityEquipmentList;
                return Json(new { patialView = RenderPartialView(this, "_ListNLTB"), partialModalView = RenderPartialView(this, "_Modal_ListNLTB") });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEQUIPMENT,
                    string.Empty,
                    ex.ToString());
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// ReadPagingCompanyAbilityFinance
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ReadPagingCompanyAbilityFinance(int page, string condition)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingCompanyAbilityFinanceList = new PaginationInfo(page);
                var companyAbilityFinanceList = await _iCompanyAbilityFinanceBL.ReadCompanyAbilityFinanceHasPagingByUserId(pagingCompanyAbilityFinanceList,
                        userProfile.UserID);
                ViewBag.companyAbilityFinanceList = companyAbilityFinanceList;
                ViewBag.pagingCompanyAbilityFinanceList = pagingCompanyAbilityFinanceList;
                return Json(new { patialView = RenderPartialView(this, "_ListNLTC"), partialModalView = RenderPartialView(this, "_Modal_ListNLTC") });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEXP,
                    string.Empty,
                    ex.ToString());
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// ReadPagingCompanyAbilityHr
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ReadPagingCompanyAbilityHr(int page, string condition)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                var pagingCompanyAbilityHrList = new PaginationInfo(page);
                var companyAbilityHrList = await _iCompanyAbilityHrBL.ReadCompanyAbilityHrHasPagingByUserId(pagingCompanyAbilityHrList,
                        userProfile.UserID);
                ViewBag.companyAbilityHrList = companyAbilityHrList;
                ViewBag.pagingCompanyAbilityHrList = pagingCompanyAbilityHrList;
                return Json(new { patialView = RenderPartialView(this, "_ListNLNS"), partialModalView = RenderPartialView(this, "_Modal_ListNLNS") });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYHR,
                    string.Empty,
                    ex.ToString());
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// Export_Profile
        /// </summary>
        /// <param name="companyProfileVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Export_Profile(CompanyProfileVM companyProfileVM)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(await _companyProfileBL.AddNewCompanyProfile(companyProfileVM, userProfile.UserID), JsonRequestBehavior.AllowGet);
                }

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYPROFILE,
                    string.Empty,
                    ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// DeleteAbilityProfile
        /// </summary>
        /// <param name="listAbilityIsDeleted"></param>
        /// <param name="typeOfAbility"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteAbilityProfile(string[] listAbilityIsDeleted, string typeOfAbility)
        {
            var methodName = SysLogger.GetMethodFullName();
            var userProfile = SessionObjects.UserProfile;
            try
            {
                IEnumerable<ResponseDeleteAbilityProfileVM> responseModel = null;
                if (listAbilityIsDeleted != null && listAbilityIsDeleted.Any() && !string.IsNullOrEmpty(typeOfAbility))
                {
                    var listId = listAbilityIsDeleted.Select(id => Convert.ToInt64(id)).Select(dummy => (long?)dummy).ToList();
                    if (typeOfAbility.Equals(typeof(CompanyAbilityExp).Name))
                    {
                        responseModel = await _iCompanyAbilityExpBL.DeleteCompanyAbilityExp(listId, typeOfAbility);
                    }
                    else if (typeOfAbility.Equals(typeof(CompanyAbilityFinance).Name))
                    {
                        responseModel = await _iCompanyAbilityFinanceBL.DeleteCompanyAbilityFinance(listId, typeOfAbility);
                    }
                    else if (typeOfAbility.Equals(typeof(CompanyAbilityEquipment).Name))
                    {
                        responseModel = await _iCompanyAbilityEquipmentBL.DeleteCompanyAbilityEquipment(listId, typeOfAbility);
                    }
                    else if (typeOfAbility.Equals(typeof(CompanyAbilityHR).Name))
                    {
                        responseModel = await _iCompanyAbilityHrBL.DeleteCompanyAbilityHr(listId, typeOfAbility);
                    }
                }
                return Json(new { succeed = CommonConstants.STR_ZERO, responseModel });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                  SessionObjects.UserProfile.UserName,
                  CommonResource.MSG_ERROR_SYSTEM,
                  CommonResource.TABLE_COMPANYPROFILE,
                  string.Empty,
                  ex.ToString());

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                        userId = userProfile.UserID
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpGet]
        public async Task<ActionResult> CapacityProfileDetail(long? id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var companyProfileVM = await _companyProfileBL.GetCompanyProfileById(id);
                return View("Edit", companyProfileVM);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEXP,
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
        public async Task<ActionResult> GetCompanyAbilityExpDetailById(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var companyAbilityExpVM = await _companyProfileBL.GetCompanyAbilityExpDetailById(id);
                return PartialView("_PopUpEditNLKN", companyAbilityExpVM);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEXP,
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
        public async Task<ActionResult> GetCompanyAbilityHrDetailById(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var companyAbilityExpVM = await _companyProfileBL.GetCompanyAbilityExpHrDetailById(id);
                return PartialView("_PopUpEditNLNS", companyAbilityExpVM);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYHR,
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
        public async Task<ActionResult> GetCompanyAbilityEquipmentDetailById(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var companyAbilityExpVM = await _companyProfileBL.GetCompanyAbilityEquipmentDetailById(id);
                List<SelectListItem> listItems = new List<SelectListItem>(_companyProfileBL.CreateAbilityEquipmentSourceListItem());
                var itemSelected = listItems.FirstOrDefault(a => a.Text == companyAbilityExpVM.Source);
                if (itemSelected != null)
                {
                    itemSelected.Selected = true;
                }
                else
                {
                    itemSelected = listItems.First();
                }

                ViewBag.itemSelected = itemSelected.Value;
                ViewBag.ListSourceItem = new SelectList(listItems, "Value", "Text", itemSelected.Value);
                return PartialView("_PopUpEditNLTB", companyAbilityExpVM);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYEQUIPMENT,
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
        public async Task<ActionResult> GetCompanyAbilityFinanceDetailById(long id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var companyAbilityExpVM = await _companyProfileBL.GetCompanyAbilityFinanceDetailById(id);
                return PartialView("_PopUpEditNLTC", companyAbilityExpVM);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_COMPANYABILITYFINANCE,
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