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
    public class RegisterController : BaseController
    {
        private readonly IRegisterBL _iRegisterBL;

        public RegisterController(IRegisterBL iRegisterBL)
        {
            _iRegisterBL = iRegisterBL;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Register
        public ActionResult Index(string returnURL)
        {
            ViewBag.returnURL = returnURL;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(LoginVM vm)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                if (ModelState.IsValid)
                {
                    //dang ky new user
                    var rs = await _iRegisterBL.CreateUser(vm);
                    if (vm.returnURL == null)
                    {
                        vm.returnURL = "/trangchu";
                    }

                    //chuyen toi page truoc do hoac home
                    return Json(new { rs = rs.AddResultStatus, returnURL = vm.returnURL });
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(string.Empty,
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

        /// <summary>
        /// AddUserInformation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUserInformation()
        {
            if (TempData["userId"] != null)
                ViewBag.Id = TempData["userId"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserInformation(RegisterUserInformationVM registerUserInformationVM)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                var result = await _iRegisterBL.UpdateUserInformation(registerUserInformationVM);

                return Json(new { rs = result.AddResultStatus});
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL,
                    CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY,
                    string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL, ex);
                return Redirect(Url.Action("Index", "Home"));
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// ConfirmEmail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                SysLogger.Info(string.Format(CommonResource.LoggerBeginMethod, methodName));
                if (userId == null || code == null)
                {
                    return Redirect(Url.Action("Index", "Home"));
                }

                var account = await _iRegisterBL.ConfirmEmailAsync(userId, code);
                if (account != null)
                {
                    return View();
                }

                return Redirect(Url.Action("Index", "Home"));
            }
            catch (Exception ex)
            {
                SysLogger.addTbActionLog(string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL,
                    CommonResource.TABLE_USERS + CommonConstants.PLUS + CommonResource.TABLE_LOGIN_HISTORY,
                    string.Empty,
                    CommonResource.ERR_CONFIRM_EMAIL, ex);
                return Redirect(Url.Action("Index", "Home"));
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }

        /// <summary>
        /// RedirectEmail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RedirectEmail()
        {
            return View();
        }
    }
}