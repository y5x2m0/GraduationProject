﻿using System;
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
    public class Topics1Controller : Controller
    {
        private AccountDBEntities db = new AccountDBEntities();

        // GET: Topics1
        public ActionResult Index()
        {
            var topic = db.Topic.Include(t => t.Paper);
            return View(topic.ToList());
        }

        // GET: Topics1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics1/Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">试卷ID</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            //ViewBag.PaperID = new SelectList(db.Paper, "PaperID", "PaperName");
            //根据试卷ID获得试卷
            Paper paper = db.Paper.Find(id);
            ViewBag.Paper = paper;
            //整理题目类型的下拉框
            List<SelectListItem> item = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1",Text="单选"},
                new SelectListItem(){ Value="2",Text="判断"},
                new SelectListItem(){ Value="3",Text="问答题"}
            };
            ViewBag.Type = new SelectList(item, "Value", "Text");
            return View();
        }


        // POST: Topics1/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,TopicExplain,TopicScore,TopicType,TopicA,TopicB,TopicC,TopicD,TopicSort,TopicAnswer,PaperID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topic.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Details","Papers1", new { id = topic.PaperID });
            }

            //ViewBag.PaperID = new SelectList(db.Paper, "PaperID", "PaperName", topic.PaperID);
            //跳转到get请求的创建方法中并将试卷ID传参带过去
            //解决了视图上初始化Paper对象为空的报错，和题目类型下拉框初始化取值报错
            return RedirectToAction("Create", new { id= topic.PaperID });
        }

        // GET: Topics1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //获得根据选择修改的考题ID获得考题
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            //根据试卷ID获得试卷
            Paper paper = db.Paper.Find(topic.PaperID);
            ViewBag.Paper = paper;
            //整理题目类型的下拉框
            List<SelectListItem> item = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1",Text="单选"},
                new SelectListItem(){ Value="2",Text="判断"},
                new SelectListItem(){ Value="3",Text="问答题"}
            };
            ViewBag.Type = new SelectList(item, "Value", "Text");
            //ViewBag.PaperID = new SelectList(db.Paper, "PaperID", "PaperName", topic.PaperID);
            return View(topic);
        }

        // POST: Topics1/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,TopicExplain,TopicScore,TopicType,TopicA,TopicB,TopicC,TopicD,TopicSort,TopicAnswer,PaperID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Papers1", new { id = topic.PaperID });
            }
            //ViewBag.PaperID = new SelectList(db.Paper, "PaperID", "PaperName", topic.PaperID);
            return RedirectToAction("Edit", new { id = topic.TopicID });
        }

        // GET: Topics1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //通过试题ID查找是否有学生已经作答
            List<Detail> dList = db.Detail.Where(p => p.TopicID == id).ToList();
            //获得试题ID对应的实体
            Topic topic = db.Topic.Find(id);
            //判断如果查找集合>0，说明试题已被学生作答，不可删除
            if (dList.Count>0)
            {
                ModelState.AddModelError("erro", "此题已作答，不可删除！");
                return View(topic);
            }
            //否则删除，并跳转到详情页
            db.Topic.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Details", "Papers1", new { id = topic.PaperID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
