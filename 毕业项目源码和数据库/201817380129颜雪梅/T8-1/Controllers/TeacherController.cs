using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T8_1.Models;
namespace T8_1.Controllers
{
    public class TeacherController : Controller
    {
        AccountDBEntities1 db = new AccountDBEntities1();
        // GET: Teacher
        public ActionResult Index()
        {
            return View(db.Teacher.ToList());
        }
        public ActionResult Show()
        {
            
            return PartialView(db.Teacher.ToList());
        }

        public ActionResult Scerch(string tName)
        {
            List<Teacher> tea = db.Teacher.Where(p => tName == "" || p.TeacherName.Contains(tName)).ToList();
            return PartialView("Scerch", tea);
        }
        public ActionResult Add(Teacher tea)
        {
           
                db.Teacher.Add(tea);
                db.SaveChanges();
                return RedirectToAction("Index");

        }
        public void Del(int tID)
        {
            var teacher = db.Teacher.Find(tID);
            db.Teacher.Remove(teacher);
            db.SaveChanges();
        }

        public ActionResult Detail(int? tID)
        {
            var teacher = db.Teacher.Find(tID);
            var json = Json(teacher);  //返回json格式  键：值
            return json;
        }
        public ActionResult Edit(Teacher tea)
        {
            db.Entry(tea).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}