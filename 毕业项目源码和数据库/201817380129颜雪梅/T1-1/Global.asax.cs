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
            //����--APP_Start��FilterConfig
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //����·��APP_Start��RouteConfig
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //���ļ����󣬼��趨��ĳ���ļ�����һ����APP_Start��BundleConfig
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
