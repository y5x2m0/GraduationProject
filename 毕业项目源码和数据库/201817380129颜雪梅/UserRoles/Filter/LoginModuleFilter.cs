using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserRoles.Filter
{
    public class LoginModuleFilter : IHttpModule
    {
        
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            //原因：AcquireRequestState 它能获取session，session可用
            context.AcquireRequestState += Context_AcquireRequestState;
        }
        /// <summary>
        /// 处理事件（拦截登录的过滤器）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_AcquireRequestState(object sender, EventArgs e)
        {
            //获得应用请求
            HttpApplication app = sender as HttpApplication;
            //可以点到4个内置对象,获取有关当前请求的http特定信息
            HttpContext context = app.Context;
            //获得浏览器请求的路径:https://……
            string Url = context.Request.Url.ToString();
            //不过滤css\is\jpg\png\fonts
            if (Url.ToLower().Contains("css") || Url.ToLower().Contains("js") 
                || Url.ToLower().Contains("jpg") || Url.ToLower().Contains("png") || Url.ToLower().Contains("gif")
                || Url.ToLower().Contains("fonts"))
            {
                return;
            }
            else 
            {
                //判断地址中是否包含/home/login，不包含！将请求的地址转成小写，判断是否包含/home/login一段路径，取反则是不包含，做处理
                if (!Url.ToLower().Contains("/home/login"))
                {
                    if (context.Session["user"] == null)
                    {
                        //发送地址到浏览器端请求到登录地址
                        context.Response.Redirect("/Home/Login");
                    }
                }
            }
            
        }
    }
}