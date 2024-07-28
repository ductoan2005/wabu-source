using System;
using System.Web.Mvc;
using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels.BiddingNews;
using FW.ViewModels.BiddingNewsRegistration;
using WABU.Filters;
using WABU.Utilities;
using System.Drawing;
using FW.Models;
using System.Collections.Generic;
using System.Linq;
using FW.Common.Enum;
using System.Threading.Tasks;
using System.IO;

namespace WABU.Controllers
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_BIDDING_NEWS_REGISTRATION, NeedReloadData = true)]
    public class BiddingNewsRegistrationController : BaseController
    {
        #region Property
        private readonly IBiddingNewsBL _biddingNewsBL;
        private readonly IBiddingNewsRegistrationBL _biddingNewsRegistrationBL;
        private readonly ICompanyProfileBL _companyProfileBL;
        private readonly ICompanyMasterBL _companyMasterBL;
        private readonly ICompanyAbilityExpBL _companyAbilityExpBL;
        private readonly ICompanyAbilityFinanceBL _companyAbilityFinanceBL;
        private readonly ICompanyAbilityHrBL _companyAbilityHrBL;
        private readonly ICompanyAbilityEquipmentBL _companyAbilityEquipmentBL;
        private readonly IConstructionBL _constructionBL;
        private readonly ICompanyBL _companyBl;
        private readonly IBiddingDetailBL _biddingDetailBl;
        private readonly IAreaManageBL _areaManageBL;

        #endregion

        #region Ctor
        public BiddingNewsRegistrationController(
                    IBiddingNewsRegistrationBL biddingNewsRegistrationBL,
                    ICompanyProfileBL companyProfileBL,
                    ICompanyMasterBL companyMasterBL,
                    ICompanyAbilityExpBL companyAbilityExpBL,
                    ICompanyAbilityFinanceBL companyAbilityFinanceBL,
                    ICompanyAbilityHrBL companyAbilityHrBL,
                    ICompanyAbilityEquipmentBL companyAbilityEquipmentBL,
                    IBiddingNewsBL biddingNewsBL,
                    IConstructionBL constructionBL,
                    ICompanyBL companyBl,
                    IBiddingDetailBL biddingDetailBl,
                    IAreaManageBL areaManageBL)
        {
            _biddingNewsBL = biddingNewsBL;
            _biddingNewsRegistrationBL = biddingNewsRegistrationBL;
            _companyProfileBL = companyProfileBL;
            _companyMasterBL = companyMasterBL;
            _companyAbilityExpBL = companyAbilityExpBL;
            _companyAbilityFinanceBL = companyAbilityFinanceBL;
            _companyAbilityHrBL = companyAbilityHrBL;
            _companyAbilityEquipmentBL = companyAbilityEquipmentBL;
            _constructionBL = constructionBL;
            _companyBl = companyBl;
            _biddingDetailBl = biddingDetailBl;
            _areaManageBL = areaManageBL;
        }
        #endregion

        #region Action
        // GET: BiddingNewsRegistration
        public ActionResult Index()
        {
            if (SessionObjects.UserProfile == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // get data select control công trình
            var constructionVMs = _constructionBL.GetSelectListConstruction(SessionObjects.UserProfile.UserID).ToList();
            ViewBag.ConstructionVMs = constructionVMs;

            // get data select control gói thầu
            var biddingPackageList = SelectListItemControl.ListBiddingPackageNameForCreateAndEdit();
            ViewBag.BiddingPackage = biddingPackageList;

            // get data select control hình thức hợp đồng
            var contractForm = SelectListItemControl.ListContractFormForCreateAndEdit();
            ViewBag.ContractForm = contractForm;

            ViewBag.selectListConstructionForm = SelectListItemControl.GetListConstructionFormForView();

            return View();
        }

        [HttpPost]
        public ActionResult GetAllConstructionByUserId()
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                long? userId = SessionObjects.UserProfile.UserID;
                var constructionVMs = _constructionBL.GetSelectListConstruction(userId).ToList();
                ViewBag.ConstructionVMs = constructionVMs;
                return Json(new { partialView = RenderPartialView(this, "_ListConstruction") });
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

        public ActionResult ReadAllWorkContentByBiddingPackage(long? Id)
        {
            // get data select control noi dung cong viec
            var workContent = _biddingNewsRegistrationBL.ReadAllWorkContentByBiddingPackage(Id);
            ViewBag.WorkContent = workContent;

            return Json(new { partialView = RenderPartialView(this, "_SelectWorkContent") });
        }

        [HttpPost]
        public async Task<ActionResult> Create(BiddingNewsRegistrationVM biddingNewsRegistrationVM)
        {
            var userProfile = SessionObjects.UserProfile;

            try
            {
                if (biddingNewsRegistrationVM != null)
                {
                    return Json(await _biddingNewsRegistrationBL.CreateBiddingNewsReturnBiddingNewsId(biddingNewsRegistrationVM, userProfile), JsonRequestBehavior.AllowGet);
                }

                return null;
            }
            catch (Exception ex)
            {
                ExportMsgExcaption(ex);
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
        }

        [HttpPost]
        public ActionResult GetDataConstructionDetail(long idConstruction)
        {
            try
            {
                var constructionVM = _constructionBL.GetConstructionDetailById(idConstruction);

                return Json(new { partialView = RenderPartialView(this, "_PopUpConstructionDetail", constructionVM) });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }

        [HttpPost]
        public ActionResult GetImageConstruction(long idConstruction)
        {
            try
            {
                var constructionVM = _constructionBL.GetConstructionDetailById(idConstruction);

                return Json(new { partialView = RenderPartialView(this, "_PopUpConstructionDetail", constructionVM) });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
        }

        public ActionResult GetImageContructionById(long? idConstruction)
        {
            var img = _constructionBL.GetConstructionDetailById(idConstruction.Value);
            return Json(new { partialView = RenderPartialView(this, "_ImageConstruction", img) });
        }

        #endregion
    }
}