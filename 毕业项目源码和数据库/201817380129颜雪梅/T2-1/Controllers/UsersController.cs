using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T2_1.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show()
        {
            return View();
        }
        public ActionResult Test()
        {
            //ViewData,ViewBag是同一集合，可以互用,不能跨动作读取（方法，视图）
            ViewData["Date"] = DateTime.Now;
            ViewBag.time = DateTime.Now;
            //TempData：1.可以跨动作读取（方法，视图）。2.只能读取一次，用完就销毁
            TempData["content"] ="TempData-content";
            //ViewBag，存储的类型为dynamic动态类型
            return View();
            
        }
       
    }
}