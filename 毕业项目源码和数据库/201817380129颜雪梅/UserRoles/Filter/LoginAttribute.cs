using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace UserRoles.Filter
{
    /// <summary>
    /// 过滤器 类命名规则：名+Attribute
    /// 需要引用using System.Web.Mvc;
    /// 需要继承ActionFilterAttribute
    /// </summary>
    public class LoginAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 正在请求时的，进行响应
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            //如果获得请求中session["user"]里的值为空，就跳转到登录页面
            if (HttpContext.Current.Session["user"]==null)
            {
                //HttpContext.Current.Response.Redirect("/Home/Login");
                //筛选器捕获请求的结果，地址转向
                filterContext.Result = new RedirectResult("/Home/Login");
            }
            
        }
    }
}