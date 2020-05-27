using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
namespace UserRoles.Controllers
{
    public class StudentsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Students
        public ActionResult Index()
        {
            List<Student> s = db.Student.ToList();
            return View(s);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                //判断注册时填写的登录账号是否在数据库存在
                Student s1 = db.Student.SingleOrDefault(p=>p.StuLoginName==s.StuLoginName);
                if (s1==null)
                {
                    db.Student.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }else
                {
                    ModelState.AddModelError("flag", "改账号已存在！");
                    return View();
                }
            }
            else
            {

                return View();
            }
            
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Student s = db.Student.Find(id);
            ViewBag.s = s;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Student s)
        {
            db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Edit(int id)
        //{
        //    Student s = db.Student.Find(id);//Find只能查主键
        //    Student s1 = db.Student.SingleOrDefault(p => p.StuID == id);
        //    Student s2 = db.Student.FirstOrDefault(p => p.StuID == id);
        //    Student s3 = db.Student.Where(p => p.StuID == id).SingleOrDefault();
        //    return View(s);
        //}
        //public ActionResult Edit(Student s)
        //{
        //    return View();
        //}
        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Student s = db.Student.Find(id);
            ViewBag.s = s;
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Student s = db.Student.Find(id);
            db.Student.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}