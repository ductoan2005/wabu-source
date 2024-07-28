using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WABU.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        [OutputCache(Duration = 86400, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Index()
        {
            return View();
        }
    }
}