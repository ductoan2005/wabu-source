using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Models;
using FW.Resources;
using FW.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WABU.Utilities;

namespace WABU.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILoginBL loginBL;

        public LoginController(ILoginBL loginBL)
        {
            this.loginBL = loginBL;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        [HttpGet]
        public ActionResult Index(string returnURL)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));

                var date = DateTime.Now.AddDays(-CommonSettings.PeroidTimeKeepLogFile);
                var task = new CleanupLog();
                // Call clean log funtion within days
                task.CleanUp(date);

                Session.Abandon();
                ViewBag.returnURL = returnURL;
                return View();
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpPost]
        public ActionResult AuthorityCheck(LoginVM loginVM, string returnURL = null)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                string result;
                if (!ModelState.IsValid)
                {
                    Session.Abandon();
                    return View("Index", loginVM);
                }

                // add config expired password date.
                //int passwordExpiredDay = CommonSettings.PasswordExpiredDay;
                UserProfile account = new UserProfile();
                //result = loginBL.CheckPassword(login, passwordExpiredDay, ref account);
                result = loginBL.CheckPassword(loginVM, ref account);

                #region check wrong password over 3 time and expried
                // Check Wrong password over 3 time.
                //if (result == LoginResource.LoginMsgErrorId3)
                //{
                //    Session.Abandon();
                //    //Lock account
                //    return Json(result);
                //}

                // Warning when PasswordChangeDate expried within days
                //if (account.PasswordChangedDate != null)
                //{
                //    TimeSpan timeRemainToNotify;
                //    timeRemainToNotify = DateTime.Now - account.PasswordChangedDate.Value.AddDays(passwordExpiredDay - CommonSettings.PasswordExpiredDurationWarning);
                //    int dayRemainToNotify = timeRemainToNotify.Days;
                //    if (result == LoginResource.LoginMsgErrorId9)
                //    {
                //        SessionObjects.UserProfile = account;
                //        var returnObj = new
                //        {
                //            ResultMessageWarn = String.Format(LoginResource.LoginMsgError9.ToString(), dayRemainToNotify.ToString()),
                //            ResultMessageWarnId = result
                //        };
                //        return Json(returnObj);
                //    } 
                //}
                #endregion

                if (result == CommonConstants.STR_ZERO)
                {
                    ViewBag.returnURL = returnURL;
                    SessionObjects.UserProfile = account;
                    var returnObj = new
                    {
                        ResultMessageWarn = string.Empty,
                        ResultMessageWarnId = result
                    };

                    SysLogger.addTbActionLog(SessionObjects.UserProfile.UserName,
                        CommonResource.SRC_LOGIN_ACT_AuthorityCheck,
                        CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY);
                    return Json(returnObj);
                }
                else
                {
                    var returnObj = new Object();
                    switch (result)
                    {
                        case CommonConstants.SCR_LOGIN_WARN_ID_1:
                            returnObj = new
                            {
                                ResultMessageWarn = LoginMessageResource.ErrorValid,
                                ResultMessageWarnId = result
                            };
                            break;
                        case CommonConstants.SCR_LOGIN_WARN_ID_1_MINUS:
                            returnObj = new
                            {
                                ResultMessageWarn = LoginMessageResource.AccountNotExist,
                                ResultMessageWarnId = result
                            };
                            break;
                        case CommonConstants.SCR_LOGIN_WARN_ID_2:
                            returnObj = new
                            {
                                ResultMessageWarn = LoginMessageResource.AccountIsLock,
                                ResultMessageWarnId = result
                            };
                            break;
                        case CommonConstants.SCR_LOGIN_WARN_ID_5:
                            returnObj = new
                            {
                                ResultMessageWarn = LoginMessageResource.AccountLoginFailed5Times,
                                ResultMessageWarnId = result
                            };
                            break;
                        default:
                            break;
                    }

                    Session.Abandon();
                    SysLogger.addTbActionLog(loginVM.UserName,
                        CommonResource.SRC_LOGIN_ACT_AuthorityCheck,
                        CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY,
                        CommonResource.ERR_LOGIN_FAIL, string.Empty);

                    return Json(returnObj);
                }
            }
            catch (Exception ex)
            {
                SessionObjects.UserProfile = null;
                SysLogger.addTbActionLog(loginVM.UserName,
                    CommonResource.SRC_LOGIN_ACT_AuthorityCheck,
                    CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY,
                    string.Empty,
                    CommonResource.ERR_LOGIN_FAIL, ex);
                return Json("error");
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        public ActionResult TimeOutNotify(string returnURL)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));

                ViewBag.returnURL = returnURL;
                return View("Index");
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

        //logout
        public void Logout(string returnURL)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                if (SessionObjects.UserProfile != null)
                {
                    SysLogger.addTbActionLog(SessionObjects.UserProfile.UserName, CommonResource.SRC_LOGIN_ACT_Logout, CommonResource.TABLE_USERS);
                }
                Session.Abandon();
                ViewBag.returnURL = returnURL;
                if (String.IsNullOrWhiteSpace(returnURL))
                {
                    Response.Redirect("/Login/Index");
                }
                else
                {
                    Response.Redirect(returnURL);
                }
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(
                    SessionObjects.UserProfile.UserName,
                    CommonResource.SRC_LOGIN_ACT_Logout,
                    CommonResource.TABLE_USERS,
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
        /// ForgetPassword
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        /// <summary>
        /// ForgetPassword
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));

                if (email == null)
                    return new JsonResult
                    {
                        Data = new
                        {
                            code = CommonConstants.STR_ONE,
                            message = CommonConstants.ADD_ERROR,
                        }
                    };

                var result = await loginBL.ForgotPassWord(email);
                if (result)
                {
                    return View("_PV_ConfirmEmailForgetPassword");
                }

                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                    }
                };
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL,
                    CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY,
                    string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(string code, string email)
        {
            if (email == null || code == null)
                return Redirect(Url.Action("Index", "Home"));

            ViewBag.Code = code;
            ViewBag.Email = email;
            return View("_PV_ChangePassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(UserMasterVM user)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                if (ModelState.IsValid)
                {
                    var result = await loginBL.ChangePassword(user);
                    if (result)
                        return View("_PV_ConfirmChangePassword");
                   
                    return View("_PV_ChangePassword");
                }

                return View("_PV_ChangePassword");
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL,
                    CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY,
                    string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        code = CommonConstants.STR_ONE,
                        message = CommonConstants.ADD_ERROR,
                    }
                };
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }
    }
}