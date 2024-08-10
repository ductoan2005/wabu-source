using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels.BiddingNews;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Utilities;

namespace WABU.Controllers
{
    public class BidContractionDetailController : BaseController
    {
        #region Property

        private readonly IBiddingNewsRegistrationBL _biddingNewsRegistrationBl;
        private readonly IBiddingDetailBL _biddingDetailBl;
        private readonly IBiddingNewsBL _biddingNewsBl;
        private readonly ICompanyMasterBL _companyMasterBL;

        #endregion

        #region Ctor

        public BidContractionDetailController(IBiddingNewsRegistrationBL biddingNewsRegistrationBl,
            IBiddingNewsBL biddingNewsBl,
            IBiddingDetailBL biddingDetailBl,
            ICompanyMasterBL companyMasterBL)
        {
            _biddingNewsBl = biddingNewsBl;
            _biddingDetailBl = biddingDetailBl;
            _biddingNewsRegistrationBl = biddingNewsRegistrationBl;
            _companyMasterBL = companyMasterBL;
        }

        #endregion

        #region Action

        /// <summary>
        /// Get bidContractionDetail index page
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(long? Id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;

                ViewBag.UserProfile = userProfile;
                ViewBag.Authority = userProfile?.Authority;

                ViewBag.checkBiddingNewsCompleted = _biddingNewsBl.checkBiddingNewsCompleted(Id);
                var model = await _biddingNewsBl.ReadBiddingNewsById(Id, userProfile);
                model.IsOwnerBiddingNews = model.Construction.UserId == userProfile?.UserID;

                return View(model);
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

        /// <summary>
        /// Create bidding detail and detail files
        /// </summary>
        /// <param name="printInfoBiddingVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PrintInfoToBidding(PrintInfoBiddingVM printInfoBiddingVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                //check role 3 already update account info
                var company = await _companyMasterBL.GetCompanyByUserId(SessionObjects.UserProfile.UserID.Value);
                if (company != null && string.IsNullOrEmpty(company.CompanyName))
                {
                    return Json(new { succeed = CommonConstants.STR_MINUS_ONE, message = "Vui lòng cập nhật thông tin nhà thầu trước khi đấu thầu" });
                }

                await _biddingDetailBl.CreateBiddingDetail(printInfoBiddingVm);

                return Json(new { succeed = CommonConstants.STR_ZERO });
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
        public async Task<ActionResult> ConfirmContractorBidding(long? biddingNewsId, long? companyProfileId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                return await _biddingDetailBl.ConfirmContractorBidding(biddingNewsId, companyProfileId);
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
        public async Task<ActionResult> CancelContractorBidding(long? biddingNewsId, long? companyProfileId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                return await _biddingDetailBl.CancelContractorBidding(biddingNewsId, companyProfileId);
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
        /// GetBiddingInfoDetailById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetBiddingInfoDetailById(long? id, long? biddingNewsId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                return PartialView("_PV_EditBiddingInfo", await _biddingDetailBl.GetBiddingDetailToEditById(id, biddingNewsId, userProfile.UserID));
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_BIDDINGDETAIL,
                    string.Empty,
                    ex.ToString());
                throw;
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// UpdateBiddingInfoDetail
        /// </summary>
        /// <param name="printInfoBiddingVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBiddingInfoDetail(PrintInfoBiddingVM printInfoBiddingVm)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                await _biddingDetailBl.UpdateBiddingInfoDetail(printInfoBiddingVm);
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.MSG_ERROR_SYSTEM,
                    CommonResource.TABLE_BIDDINGDETAIL,
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
        public async Task<ActionResult> DeleteBiddingDetailById(long? id)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                await _biddingDetailBl.DeleteBiddingInfoDetail(id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Download(string path)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Download the file content from the given URL
                    using (HttpResponseMessage httpResponse = await client.GetAsync(path))
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            using (Stream stream = await httpResponse.Content.ReadAsStreamAsync())
                            {
                                // Set content type and headers based on the file type
                                Response.ContentType = httpResponse.Content.Headers.ContentType.ToString();
                                string fileName = Path.GetFileName(path);
                                Response.AddHeader("Content-Disposition", $"attachment; filename=\"{fileName}\"");

                                // Stream the file content directly to the response
                                await stream.CopyToAsync(Response.OutputStream);
                            }

                            Response.Flush();
                        }
                        else
                        {
                            Response.StatusCode = (int)httpResponse.StatusCode;
                            Response.Write($"Failed to download file: {httpResponse.ReasonPhrase}");
                        }
                    }
                }
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
        /// CompletedContractorBidding
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveBiddingNewsBookMark(long? biddingNewsId)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var userProfile = SessionObjects.UserProfile;
                return await _biddingNewsBl.SaveBiddingNewsBookmark(biddingNewsId, userProfile);
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