using FW.Common.Helpers;
using FW.Resources;
using System;
using System.IO;
using System.Web.Mvc;

namespace WABU.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected ActionResult ExportMsgExcaption(Exception ex)
        {
            if (ex is CommonExceptions)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(CommonResource.MSG_ERROR_SYSTEM, JsonRequestBehavior.AllowGet);
            }
        }

        protected string RenderPartialView(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
    }
}