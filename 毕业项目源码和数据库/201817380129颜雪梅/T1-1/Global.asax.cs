using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace T1_1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //过滤--APP_Start的FilterConfig
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //配置路由APP_Start的RouteConfig
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //将文件捆绑，家设定的某种文件捆成一捆，APP_Start的BundleConfig
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
