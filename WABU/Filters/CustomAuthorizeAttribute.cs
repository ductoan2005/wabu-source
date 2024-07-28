using FW.BusinessLogic.Interfaces;
using FW.Common.Enum;
using FW.Common.Helpers;
using System.Net;
using System.Web.Mvc;
using WABU.Utilities;

namespace WABU.Filters
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private static readonly object objToLock = new object();

        public string FunctionKey { get; set; }

        public bool NeedReloadData { get; set; }

        public ILoginBL LoginBL { get; set; }

        public void OnAuthorization(AuthorizationContext context)
        {
            lock (objToLock)
            {
                if (NeedReloadData && SessionObjects.UserProfile != null)
                {
                    var user = LoginBL.GetUserById(SessionObjects.UserProfile.UserID);
                    // check whether authority is changed
                    if (user != null)
                    {
                        user.OldAuthority = SessionObjects.UserProfile.Authority;
                    }

                    SessionObjects.UserProfile = user;
                }
            }

            // check whether user logged in or not
            if (SessionObjects.UserProfile == null)
            {
                // clear all session variables
                System.Web.HttpContext.Current.Session.Abandon();

                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    System.Web.HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    System.Web.HttpContext.Current.Response.End();
                    context.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    var routeValues = new System.Web.Routing.RouteValueDictionary
                                            {
                                                {
                                                    "controller",
                                                    "Login"
                                                },
                                                {
                                                    "action",
                                                    "TimeOutNotify"
                                                },
                                                {
                                                    "returnUrl", context.RequestContext.HttpContext.Request.Url
                                                }
                                            };

                    context.Result = new RedirectToRouteResult(
                                            "Default",
                                            routeValues
                                            );
                }
            }
            else
            {
                // check whether user has access right or not
                if (!AccessRight.CheckRole((EAuthority)SessionObjects.UserProfile.Authority, this.FunctionKey))
                {
                    if (context.HttpContext.Request.IsAjaxRequest())
                    {
                        JsonResult result = new JsonResult();

                        // notify that user does not have aceess right
                        System.Web.HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        result.Data = CommonConstants.AUTHORITY_NOT_MATCH;
                        context.Result = result;
                    }
                    else
                    {
                        context.Result = new RedirectToRouteResult(
                                            "Default",
                                            new System.Web.Routing.RouteValueDictionary
                                        {
                                                {
                                                    "controller",
                                                    "Home"
                                                },
                                                {
                                                    "action",
                                                    "Index"
                                                },
                                        });
                    }
                }
            }
        }
    }
}