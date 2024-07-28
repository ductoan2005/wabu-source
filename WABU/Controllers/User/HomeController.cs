using FW.BusinessLogic.Interfaces;
using System.Web.Mvc;
using System.Web.UI;
using WABU.Utilities;

namespace WABU.Controllers
{
    //[CustomAuthorize(FunctionKey = CommonConstants.SCREEN_HOME, NeedReloadData = true)]
    public class HomeController : BaseController
    {
        #region Property
        private readonly IHomeBL _homeBL;
        #endregion

        #region Ctor
        public HomeController(IHomeBL homeBL)
        {
            this._homeBL = homeBL;
        }
        #endregion

        #region Action
        // GET: Home
        public ActionResult Index()
        {
            // get gói thầu control select
            // get data select control công trình
            var biddingPackageList = SelectListItemControl.ListBiddingPackageNameForView();
            ViewBag.BiddingPackage = biddingPackageList;

            //var biddingPackage = _homeBL.ReadBiddingPackage();
            //ViewBag.BiddingPackage = biddingPackage;

            // get (tin tức)Gói Thầu Tốt Nhất
            var biddingNewsBest = _homeBL.ReadBiddingNewsBest();
            ViewBag.BiddingNewsBest = biddingNewsBest;

            // get (tin tức)Gói Thầu Mới Nhất
            var biddingNewsNewest = _homeBL.ReadBiddingNewsNewest();
            ViewBag.BiddingNewsNewest = biddingNewsNewest;

            // get (tin tức)Gói Thầu Được Quan Tâm Nhất
            var biddingNewsInterest = _homeBL.ReadBiddingNewsInterest();
            ViewBag.BiddingNewsInterest = biddingNewsInterest;

            // get (tin tức)Gói Thầu Được Đề Xuất
            //var biddingNewsSuggest = _homeBL.ReadBiddingNewsSuggest();
            //ViewBag.BiddingNewsSuggest = biddingNewsSuggest;

            // get danh sách nhà thầu
            var compContructor = _homeBL.GetCompanyRating();
            ViewBag.CompanyReputation = compContructor;

            // get danh sách logo nhà thầu nổi bật
            var complogoOnOver = _homeBL.ReadCompayLogoOnOver();
            ViewBag.ReadCompayLogoOnOver = complogoOnOver;

            return View("Index");
        }

        [HttpGet]
        [OutputCache(Duration = 86400, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Advisory()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 86400, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Security()
        {
            return View();
        }
        #endregion
    }
}