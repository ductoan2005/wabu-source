using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WABU.Controllers.User
{
    public class CompanyWebsiteHTMLController : BaseController
    {
        // GET: CompanyWebsiteHTML
        public ActionResult Index(long id)
        {
            ViewBag.idnhathau = id;
            return View();
        }
    }
}