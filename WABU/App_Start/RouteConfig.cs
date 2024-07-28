using System.Web.Mvc;
using System.Web.Routing;

namespace WABU
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region BiddingNewsBookmark

            routes.MapRoute(
              name: "Tin thau quan tam",
              url: "tinthau/quantam",
              defaults: new { controller = "BiddingNewsBookmark", action = "Index" }
            );

            #endregion

            #region FilterBiddingNews

            routes.MapRoute(
              name: "Duyet tin",
              url: "duyettin",
              defaults: new { controller = "FilterBiddingNews", action = "Index" }
            );

            routes.MapRoute(
              name: "Quan Ly Tin Thau",
              url: "quanly-tinthau",
              defaults: new { controller = "FilterBiddingNews", action = "IndexInvalid" }
            );

            #endregion About

            #region About

            routes.MapRoute(
              name: "About",
              url: "about",
              defaults: new { controller = "About", action = "Index" }
            );

            #endregion About

            #region BiddingNewsRegistration

            routes.MapRoute(
              name: "Dang Ky Tin Thau",
              url: "tinthau/dangky",
              defaults: new { controller = "BiddingNewsRegistration", action = "Index", returnURL = UrlParameter.Optional }
            );

            #endregion BiddingNewsRegistration

            #region Construction

            routes.MapRoute(
              name: "Cong Trinh",
              url: "congtrinh",
              defaults: new { controller = "Construction", action = "Index", returnURL = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "quan ly cong trinh",
            url: "quanly/congtrinh",
            defaults: new { controller = "ConstructionManagement", action = "Index", id = UrlParameter.Optional }
          );

            #endregion Construction

            #region Account

            routes.MapRoute(
            name: "quan ly tai khoan",
            url: "taikhoan/capnhat/{id}",
            defaults: new { controller = "Account", action = "ProfileManagement", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "Danh Sach Tin Thau",
             url: "tinthau/quanly",
             defaults: new { controller = "Account", action = "PagePackage", biddingNewsId = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Nha Thau Ho So",
             url: "hoso/quanly/{biddingNewsId}",
             defaults: new { controller = "Account", action = "PageContract", biddingNewsId = UrlParameter.Optional }
           );

            #endregion Account

            #region PageContractBid

            routes.MapRoute(
              name: "Dang Ky Ho So",
              url: "hoso/dangky",
              defaults: new { controller = "PageContractBid", action = "Index", returnURL = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Chinh Sua Ho So",
              url: "hoso/chinhsua/{id}",
              defaults: new { controller = "PageContractBid", action = "CapacityProfileDetail", id = UrlParameter.Optional }
            );

            #endregion PageContractBid

            #region Login

            routes.MapRoute(
              name: "Dang Xuat",
              url: "dangxuat",
              defaults: new { controller = "Login", action = "Logout", returnURL = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Dang Nhap",
              url: "dangnhap",
              defaults: new { controller = "Login", action = "Index", returnURL = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Quen Mat Khau",
              url: "forgotpassword",
              defaults: new { controller = "Login", action = "ForgetPassword"}
            );

            routes.MapRoute(
             name: "Thay Doi Mat Khau",
             url: "changepassword",
             defaults: new { controller = "Login", action = "ChangePassword", code = UrlParameter.Optional, email = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Xac Thuc Dang Ky",
             url: "confirm",
             defaults: new { controller = "Register", action = "ConfirmEmail", code = UrlParameter.Optional, userId = UrlParameter.Optional }
           );

            #endregion Login

            #region Register

            routes.MapRoute(
            name: "Them thong tin tai khoan",
            url: "themthongtintaikhoan",
            defaults: new { controller = "Register", action = "AddUserInformation", returnURL = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "Dang Ky",
             url: "dangky",
             defaults: new { controller = "Register", action = "Index", returnURL = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Xac Nhan Dang Ky Email",
             url: "confirm/{code}/{userId}",
             defaults: new { controller = "Register", action = "ConfirmEmail", userId = UrlParameter.Optional, code = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Xac Nhan Email",
             url: "confirmEmail",
             defaults: new { controller = "Register", action = "RedirectEmail"}
           );

            #endregion Register

            routes.MapRoute(
              name: "Ho So Nha Thau",
              url: "hoso/chitiet/{id}/{biddingNewsId}",
              defaults: new { controller = "BidShowContractDetail", action = "Index", id = UrlParameter.Optional, biddingNewsId = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Chi Tiet Tin Thau",
               url: "tinthau/chitiet/{id}",
               defaults: new { controller = "BidContractionDetail", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Tin Thau Tim Kiem",
              url: "tinthau/timkiem",
              defaults: new { controller = "BiddingNews", action = "Search" }
            );

            #region Home

            routes.MapRoute(
               name: "trang chu dieu khoan",
               url: "dieukhoan",
               defaults: new { controller = "Home", action = "Advisory" }
           );

            routes.MapRoute(
               name: "trang chu bao mat",
               url: "baomat",
               defaults: new { controller = "Home", action = "Security" }
           );

            routes.MapRoute(
              name: "trang chu",
              url: "trangchu",
              defaults: new { controller = "Home", action = "Index" }
          );

            #endregion Home

            #region Contractor

            routes.MapRoute(
               name: "nha thau html",
               url: "nhathau/{name}_{id}",
               defaults: new { controller = "CompanyWebsiteHTML", action = "Index", id = UrlParameter.Optional }
           );

            #endregion Contractor

            #region User Master

            routes.MapRoute(
            name: "quan ly tai khoan admin",
            url: "quanly/taikhoan",
            defaults: new { controller = "UserMaster", action = "Index", id = UrlParameter.Optional }
          );

            #endregion User Master

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}