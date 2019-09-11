using ESafety.Core;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ESafety.Core.Model.DB;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ESafety.Platform.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            AOP.reg();

            var auth = new Auth_UserService(new Unitwork(new ESFdb())).GetAllAuth("");
            Web.Unity.AuthKey.AuthKeys = auth.data.ToList();
        }
    }
}
