using System;
using FW.Common.Utilities;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WABU.Utilities;
using static FW.Resources.CommonResource;
using System.Configuration;
using System.Data.SqlClient;

namespace WABU
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["FWDbContext"].ConnectionString;
        protected void Application_Start()
        {
            

            // load config for log4net
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Log4Net.config")));

            SysLogger.Info(LoggerAppStart);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacBootstrapper.Run();
            //Start SqlDependency with application initialization
            //SqlDependency.Start(connString);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}
