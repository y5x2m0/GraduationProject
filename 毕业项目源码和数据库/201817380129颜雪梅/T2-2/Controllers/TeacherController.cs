using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2_2.Models;
namespace T2_2.Controllers
{
    public class TeacherController : Controller
    {
        ExamDBEntities db = new ExamDBEntities();
        // GET: Teacher
        public ActionResult Index()
        {
            //view传参
            List<Teacher> list = db.Teacher.ToList();
            //ViewBag.List = list;
            ViewData["List"] = list;
            return View(list) ;
        }
        public ActionResult Show()
        {
            //获得请求地址的参数id
            string id = Request["id"];
            //根据id查询数据库存储中的对象
            Teacher teacher= db.Teacher.Find(int.Parse(id));
            ViewBag.teacher = teacher;
            return View();
        }
        public ActionResult Delete(string id)
        {
            Teacher teacher= db.Teacher.Find(int .Parse(id));
           //移除
            db.Teacher.Remove(teacher);
            //执行
            db.SaveChanges();
            //转到Index（）
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        //在asp.net mvc框架中，为了限制某个action(动作-方法)只能接受httpPost的请求、httpGet的请求
        //[httpPost]:限制action只能接受httpPost的请求，对于httpGet的请求则提示404找不到页面
        //如果Action前没有[httpPost]，也没有[httpGet]，则两种方式的请求都接收
        //[HttpGet]
        [HttpPost]
        //AddAdd(Teacher teacher)form 表单的元素，name的值一定要与Teacher中的属性值一致
        public ActionResult Add(Teacher teacher)
        {
            db.Teacher.Add(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Teacher tea =  db.Teacher.Find(id);
            
            return View(tea);
        }
        /// <summary>
        /// DefaultModelBinder类自动实现默认模型绑定器
        /// 通过四种途径获得绑定值：
        /// request.Form：获得表单提交的值
        /// RouteData.Values：获得路由的值
        /// RouteData.QueryString：获得URL的值
        /// RouteData.Files：获得上传文件
        /// </summary>
        /// <param name="newtea"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(/*int id*/ Teacher NewTea)
        {
            //Teacher NewTea =db.Teacher.Find(id);
            //if (Request.Form.Count>0)
            //{
            //    NewTea.TeacherID = int.Parse(Request.Form["TeacherID"]);
            //    NewTea.TeacherLoginName = Request.Form["TeacherLoginName"];
            //    NewTea.TeacherLoginPwd = Request.Form["TeacherLoginPwd"];
            //    NewTea.TeacherName = Request.Form["TeacherName"];
            //    NewTea.TeacherPhone = Request.Form["TeacherPhone"];
            //    NewTea.TeacherEmail = Request.Form["TeacherEmail"];
            //}
            Teacher oldTea = db.Teacher.Find(NewTea.TeacherID);
            oldTea.TeacherLoginName = NewTea.TeacherLoginName;
            oldTea.TeacherLoginPwd = NewTea.TeacherLoginPwd;
            oldTea.TeacherName = NewTea.TeacherName;
            oldTea.TeacherPhone = NewTea.TeacherPhone;
            oldTea.TeacherEmail = NewTea.TeacherEmail;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Index(string teaName, string teaLoginName)
        {
            // nq方法
            // List<Teacher> list = db.Teacher.Where(p => p.TeacherName.Contains(teaName) || teaName == "").
                //Where(k => k.TeacherLoginName == teaLoginName || teaLoginName == "").ToList();
            //nq语句
             List<Teacher> list = (from tea in db.Teacher where tea.TeacherName.Contains(teaName)&& tea.TeacherLoginName == teaLoginName || teaLoginName=="" select tea).ToList();
            ViewBag.List = list;
            return View(list);
        }


    }
}