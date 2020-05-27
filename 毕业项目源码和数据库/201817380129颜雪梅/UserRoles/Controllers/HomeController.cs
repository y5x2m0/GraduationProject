using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class HomeController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Account, string Pwd)
        {
            //根据账号和密码查找用户表中唯一用户
            var user = db.UserInfos.SingleOrDefault(p => p.Account == Account && p.Pwd == Pwd);
            if (user != null)
            {
                //查找账号存入到Session对象中
                Session["user"] = user;
                //将视图作为查询对象v_User_Role_Menu，过滤，根据当前的ID，Distinct,去重复行
                List<v_User_Role_Menu> list = db.v_User_Role_Menu.Where(u=>u.UserID==user.ID).Distinct().ToList();
                Session["v_User_Role_Menu"] = list;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                return Content("<script>alert('账号或密码不正确！');history.go(-1);</script>");
            }
        }
        
       public ActionResult Exit()
        {
            Session["user"] = null;//清空Session,
            Session["v_User_Role_Menu"] = null;
            return RedirectToAction("Login", "Home");
        }
        /// <summary>
        /// 老师登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginTeacher(Teacher tea)
        {
            ViewBag.tea = tea;
            return View(tea);
        }
        [HttpPost]
        public ActionResult LoginTeacher(string TeacherLoginName,string TeacherLoginPwd)
        {
            if (ModelState.IsValid)
            {
                Teacher t1 = db.Teacher.SingleOrDefault(p => p.TeacherLoginName == TeacherLoginName && p.TeacherLoginPwd == TeacherLoginPwd);
                if (t1!=null)
                {
                    return RedirectToAction("Indextea", "Home");
                }
                else
                {
                    ModelState.AddModelError("flag","账号或密码错误");   
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
        /// <summary>
        /// 老师登录进去之后首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Indextea()
        {
            return View();
        }
        /// <summary>
        /// 学生登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginStudent(Student stu)
        {
            ViewBag.stu = stu;
            return View(stu);
        }
        [HttpPost]
        public ActionResult LoginStudent(string StuLoginName, string StuLoginPwd)
        {
            if (ModelState.IsValid)
            {
                Student s = db.Student.SingleOrDefault(p => p.StuLoginName == StuLoginName && p.StuLoginPwd == StuLoginPwd);
                Session["Session"] = s;
                if (s!=null)
                {
                    return RedirectToAction("Indextstu", "Home");
                }
                else
                {
                    ModelState.AddModelError("flag", "账号或密码错误");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
        /// <summary>
        /// 学生登录进去之后的首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Indextstu()
        {
            return View();
        }
        public ActionResult Loginout()
        {
            return View();
        }/// <summary>
         /// Ajax
         /// </summary>
         /// <returns></returns>
        public ActionResult AjaxLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckLoginName(string LoginName)
        {
            Student stu = db.Student.FirstOrDefault(p => p.StuLoginName == LoginName);
            if (stu == null)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }

        }


    }
}