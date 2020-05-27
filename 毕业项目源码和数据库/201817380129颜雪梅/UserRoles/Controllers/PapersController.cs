using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
namespace UserRoles.Controllers
{
    public class PapersController : Controller
    {
        
        AccountDBEntities db = new AccountDBEntities();
       
        // GET: Papers
        public ActionResult Index()
        {
            List<Paper> p = db.Paper.ToList();
            return View(p);
        }
        /// <summary>
        /// 添加新试卷
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Paper p)
        {

            db.Paper.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 添加考题
        /// </summary>
        /// <returns></returns>
        public ActionResult Create1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create1(Topic t)
        {
            db.Topic.Add(t);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.p= db.Paper.Find(id);
            
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Paper p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            Paper paper = db.Paper.Find(id);
            ViewBag.paper = paper;
            return View(paper);
        }

        public ActionResult Delete(int id)
        {
            List<Paper> plist = db.Paper.Where(p => p.PaperID == id).ToList();
            if (plist.Count > 0)
            {
                return Content("<script>alert('该试卷不能删除！');history.go(-1);</script>");
            }
            Paper paper = db.Paper.Find(id);
            db.Paper.Remove(paper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
