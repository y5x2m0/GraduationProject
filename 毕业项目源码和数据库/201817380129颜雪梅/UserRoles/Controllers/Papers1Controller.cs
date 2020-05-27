using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

namespace UserRoles.Controllers
{
    public class Papers1Controller : Controller
    {
        private AccountDBEntities db = new AccountDBEntities();

        // GET: Papers1
        public ActionResult Index()
        {
            return View(db.Paper.ToList());
        }

        // GET: Papers1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Paper.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            return View(paper);
        }

        // GET: Papers1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Papers1/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaperID,PaperName,PaperExplain,PaperTime")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Paper.Add(paper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paper);
        }

        // GET: Papers1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Paper.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            return View(paper);
        }

        // POST: Papers1/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaperID,PaperName,PaperExplain,PaperTime")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paper);
        }

        // GET: Papers1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Paper.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            return View(paper);
        }

        // POST: Papers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Topic> tList = db.Topic.Where(p=>p.PaperID==id).ToList();
            List<Answer> aList = db.Answer.Where(p => p.PaperID == id).ToList();
            Paper paper = db.Paper.Find(id);
            if (tList!=null)
            {
                ModelState.AddModelError("flag", "该试卷已经添加考题，不能删除！");
                return View(paper);
            }
            else if (aList!=null)
            {
                ModelState.AddModelError("flag", "该试卷已在作答，不能删除！");
                return View(paper);
            }
            
            db.Paper.Remove(paper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
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
        /// <summary>
        /// 学生考试
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexStu()
        {
            ViewBag.s = db.Student.Find(1);
            return View(db.Paper.ToList());
        }
    }
}
