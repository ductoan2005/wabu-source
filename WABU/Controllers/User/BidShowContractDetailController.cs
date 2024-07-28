using FW.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FW.Common.Helpers;
using FW.Common.Utilities;
using WABU.Filters;
using WABU.Utilities;
using System.IO;
using FW.Resources;
using FW.ViewModels;

namespace WABU.Controllers
{
    [CustomAuthorize(FunctionKey = CommonConstants.SCREEN_BID_CONTRACTION_DETAIL, NeedReloadData = true)]
    public class BidShowContractDetailController : BaseController
    {
        #region Property
        private readonly IBiddingNewsBL _biddingNewsBL;
        private readonly ICompanyProfileBL _companyProfileBL;
        private readonly ICompanyAbilityExpBL _companyAbilityExpBL;
        private readonly ICompanyAbilityFinanceBL _companyAbilityFinanceBL;
        private readonly ICompanyAbilityHrBL _companyAbilityHrBL;
        private readonly ICompanyAbilityEquipmentBL _companyAbilityEquipmentBL;
        private readonly ICompanyBL _companyBl;
        private readonly IBiddingDetailBL _biddingDetailBl;

        #endregion

        #region Ctor
        public BidShowContractDetailController(
                    ICompanyProfileBL companyProfileBL,
                    ICompanyAbilityExpBL companyAbilityExpBL,
                    ICompanyAbilityFinanceBL companyAbilityFinanceBL,
                    ICompanyAbilityHrBL companyAbilityHrBL,
                    ICompanyAbilityEquipmentBL companyAbilityEquipmentBL,
                    IBiddingNewsBL biddingNewsBL,
                    ICompanyBL companyBl,
                    IBiddingDetailBL biddingDetailBl)
        {
            _biddingNewsBL = biddingNewsBL;
            _companyProfileBL = companyProfileBL;
            _companyAbilityExpBL = companyAbilityExpBL;
            _companyAbilityFinanceBL = companyAbilityFinanceBL;
            _companyAbilityHrBL = companyAbilityHrBL;
            _companyAbilityEquipmentBL = companyAbilityEquipmentBL;
            _companyBl = companyBl;
            _biddingDetailBl = biddingDetailBl;
        }
        #endregion

        #region Action

        // GET: BidShowContractDetail
        public async Task<ActionResult> Index(long? Id, long? biddingNewsId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                var userProfile = SessionObjects.UserProfile;
                var companyProfile = await _companyProfileBL.GetCompanyProfileById(Id);
                var company = companyProfile.Company;

                // get id owner công trình
                if (biddingNewsId != null)
                {
                    var userOwnerBiddingNews = await _biddingDetailBl.GetBiddingNewsById(biddingNewsId);
                    if (userProfile.UserID == userOwnerBiddingNews?.BiddingNews.Construction.UserId)
                    {
                        var viewModel = await _biddingNewsBL.CheckBiddingNewsProfileShowCondition(biddingNewsId, Id);
                        ViewBag.BiddingNews = viewModel;
                    }
                    else
                    {
                        Response.Redirect("/trangchu");
                    }
                }

                ViewBag.listcompanyAbilityExp = _biddingDetailBl.GetAllCompanyAbilityExpByListId(companyProfile.AbilityExpsId);
                ViewBag.listcompanyAbilityFinance = _biddingDetailBl.GetAllCompanyAbilityFinanceByListId(companyProfile.AbilityFinancesId);
                ViewBag.listcompanyAbilityHr = _biddingDetailBl.GetAllCompanyAbilityHrsByListId(companyProfile.AbilityHRsId);
                ViewBag.listcompanyAbilityEquipment = _biddingDetailBl.GetAllCompanyAbilityFinanceByListId(companyProfile.AbilityEquipmentsId);
                ViewBag.company = company;
                ViewBag.Authority = userProfile.Authority;
                ViewBag.BiddingNewsId = biddingNewsId;
                ViewBag.CompanyProfileId = Id;
                ViewBag.companyProfileModel = companyProfile;

                return View();
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(string.Empty, methodName,
                    CommonResource.TABLE_BIDDING_NEWS + CommonConstants.PLUS + CommonResource.TABLE_COMPANYPROFILE,
                    string.Empty, string.Empty, ex);
                return Redirect(Url.Action("Index", "Home"));
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// companyAbilityExp
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JsonResult> CompanyAbilityExp(long? Id)
        {
            var exp = await _companyAbilityExpBL.ReadAllCompanyAbilityExpBy(Id);
            var jsonResult = Json(new
            {
                result = exp,
                EvidenceContractJson = exp.EvidenceContractFilePath,
                EvidenceBuildingPermitJson = exp.EvidenceBuildingPermitFilePath,
                EvidenceContractLiquidationJson = exp.EvidenceContractLiquidationFilePath
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// companyAbilityFinances
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JsonResult> CompanyAbilityFinances(long? Id)
        {
            var finances = await _companyAbilityFinanceBL.ReadAllCompanyAbilityFinanceBy(Id);
            var jsonResult = Json(new
            {
                result = finances,
                EvidenceCheckSettlementJson = finances.EvidenceCheckSettlementFilePath,
                EvidenceDeclareTaxJson = finances.EvidenceDeclareTaxFilePath,
                EvidenceCertificationTaxJson = finances.EvidenceCertificationTaxFilePath,
                EvidenceAuditReportJson = finances.EvidenceAuditReportFilePath
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// companyAbilityHr
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JsonResult> CompanyAbilityHr(long? Id)
        {
            var hr = await _companyAbilityHrBL.ReadAllCompanyAbilityHrBy(Id);
            string CompanyAbilityHRDetails = "";
            foreach (var item in hr.CompanyAbilityHRDetails)
            {
                CompanyAbilityHRDetails += @"<h3 class='title-news'>" + item.ProjectSimilar + "</h3><div class='div-content'><p class='mb5'>Từ ngày: " + item.FromYear.Value.ToString("dd/MM/yyyy") + " - Đến ngày: " + item.ToYear.Value.ToString("dd/MM/yyyy") + " </p><p class='mb5'>Vị trí: " + item.PositionSimilar + "</p></div><hr />";
            }
            hr.CompanyAbilityHRDetails = null;
            var jsonResult = Json(new
            {
                result = hr,
                EvidenceLaborContractJson = hr.EvidenceLaborContractFilePath,
                EvidenceSimilarCertificatesJson = hr.EvidenceSimilarCertificatesFilePath,
                EvidenceAppointmentStaffJson = hr.EvidenceAppointmentStaffFilePath,
                companyabilityHRDetails = CompanyAbilityHRDetails
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        /// companyAbilityEquipment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JsonResult> CompanyAbilityEquipment(long? Id)
        {
            var equipment = await _companyAbilityEquipmentBL.ReadAllCompanyAbilityEquipmentBy(Id);
            var jsonResult = Json(new { result = equipment, EvidenceSaleContractJson = equipment.EvidenceSaleContractFilePath, EvidenceInspectionRecordsJson = equipment.EvidenceInspectionRecordsFilePath }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Download(string path)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var fileFullPath = Server.MapPath(path);
                FileInfo fileInfo = new FileInfo(fileFullPath);
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileInfo.Name);
                Response.TransmitFile(fileFullPath);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// Investor choose contractor from bidcontractshowdetail page
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ChooseContractorBidding(long? biddingNewsId, long? companyProfileId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                return await _biddingDetailBl.ChooseContractorBidding(biddingNewsId, companyProfileId);
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

        /// <summary>
        /// CompletedContractorBidding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CompletedContractorBidding(long? biddingNewsId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                return await _biddingDetailBl.CompletedContractorBidding(biddingNewsId);
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
        public async Task<ActionResult> RatingStarForCompany(long companyProfileId, byte star)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                await _companyBl.RatingStarForCompany(companyProfileId, star);
                return Json(new
                {
                    result = CommonConstants.STR_ZERO
                }, JsonRequestBehavior.AllowGet);
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