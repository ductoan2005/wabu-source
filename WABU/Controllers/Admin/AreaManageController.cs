using FW.BusinessLogic.Interfaces;
using FW.Common.Utilities;
using FW.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WABU.Utilities;

namespace WABU.Controllers.Admin
{
    public class AreaManageController : Controller
    {
        public readonly IAreaManageBL _areaManageBL;

        public AreaManageController(IAreaManageBL areaManageBL)
        {
            _areaManageBL = areaManageBL;
        }

        // GET: AreaManage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 86400, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult GetAllArea()
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                var lstarea = _areaManageBL.GetAllArea();
                return Json(lstarea, JsonRequestBehavior.AllowGet);
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