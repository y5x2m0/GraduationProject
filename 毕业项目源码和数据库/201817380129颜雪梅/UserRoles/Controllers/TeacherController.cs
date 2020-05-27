using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
namespace UserRoles.Controllers
{
    public class TeacherController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Teacher
        public ActionResult Index()
        {
            List<Teacher> tList = db.Teacher.ToList();
            return View(tList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher tea)
        {
           
                db.Teacher.Add(tea);
                db.SaveChanges();
                return RedirectToAction("Index");
          
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Teacher tea = db.Teacher.Find(id);
            ViewBag.tea = tea;
            
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Teacher tea)
        {
            db.Entry(tea).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Teacher tea = db.Teacher.Find(id);
            ViewBag.tea = tea;
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Teacher tea = db.Teacher.Find(id);
            db.Teacher.Remove(tea);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ArrayList arr = new ArrayList();
            string rkeyStr = "";
            StringBuilder sb = new StringBuilder();
            if (Request["delItems"] != null && Request["delItems"].ToString() != "")
            {
                rkeyStr = Request["delItems"].ToString();
                string[] rkeyArr = rkeyStr.Split(',');
                int count = 0;
                for (int i = 0; i < rkeyArr.Length; i++)
                {
                    int value = int.Parse(rkeyArr[i]);
                    var R_user = db.Answer.Where(p => p.TeacherID == value).FirstOrDefault();

                    if (R_user != null)
                    {
                        string str = "该老师已设置绑定，不可删除，如果想要删除该用户请清除该老师的绑定！";
                        return Content(str);
                    }
                    else
                    {
                        Teacher tea = db.Teacher.Find(int.Parse(rkeyArr[i]));
                        db.Teacher.Remove(tea);
                    }
                }
                count = db.SaveChanges();
                if (count > 0)
                {
                    string str = "批量删除成功！删除了"+count+"条数据";
                    return Content(str);
                }
            }
            else
            {
                rkeyStr = "";
                string str = "批量删除失败！";
                return Content(str);
            }
            return null;
        }


    }

}