using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
using UserRoles.Filter;
namespace Account.Controllers
{
    [Login]
    public class DepartmentController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Department
        public ActionResult Index(string Name = "")
        {
            List<Departments> list = db.Departments.Where(p => p.Name == "" || p.Name.Contains(Name)).ToList();
            return View(list);
        }

        public ActionResult Add(Departments dep)
        {
            db.Departments.Add(dep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            Departments dep = db.Departments.Find(ID);
            ViewBag.dep = dep;

            db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Departments dep)
        {
            db.Entry(dep).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmentsDelete(int DepartmentID)
        {
            var rURList = db.UserInfos.Where(p => p.DepartmentID == DepartmentID).ToList();
            if (rURList.Count > 0)
            {
                return Content("<script>alert('该部门有用户不能删除');history.go(-1)</script>");
            }
            else
            {
                Departments dep = db.Departments.Find(DepartmentID);
                db.Departments.Remove(dep);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
