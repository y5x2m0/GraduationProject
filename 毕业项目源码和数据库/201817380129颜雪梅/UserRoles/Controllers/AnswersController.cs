using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;

using PagedList;
namespace UserRoles.Controllers
{
    public class AnswersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Answers
        public ActionResult Index(int PaperID,int StuID)
        {
            //根据学生的ID和试卷ID获得答题信息
            Answer answer = db.Answer.FirstOrDefault(a => a.PaperID == PaperID && a.StuID == StuID);

            return View(answer);
        }
        [HttpPost]
        public void Index(int AnswerID, int TopicID, string DetailAnswer)
        {
            var result = db.Detail.FirstOrDefault(d => d.TopicID == TopicID && d.AnswerID == AnswerID);
            result.DetailAnswer = DetailAnswer;
            db.SaveChanges();
        }
        public ActionResult SubmitPaper(int AnswerID)
        {
            Answer answer = db.Answer.Find(AnswerID);
            answer.SubmitTime = DateTime.Now;
            answer.AnswerState = 1;
            db.SaveChanges();
            return RedirectToAction("IndexStu", "Papers1");
        }



        public ActionResult AnswersDetail(int PaperID,int StuID)
        {
            //初始化答题信息
            Answer an = new Answer()
            {
                PaperID = PaperID,
                StuID = StuID,
                TeacherID = 1,
                AnswerScore = 0,
                AnswerTime = DateTime.Now,
                SubmitTime=null,
                BatchTime=null,
                AnswerState=0
            };
            db.Answer.Add(an);
            db.SaveChanges();
            List<Topic> tList = db.Topic.Where(t => t.PaperID == PaperID).ToList();
            foreach (var topic in tList)
            {
                Detail detail = new Detail()
                {
                     AnswerID=an.AnswerID,
                     TopicID=topic.TopicID,
                     DetailAnswer=""
                };
                db.Detail.Add(detail);
               
            }
            db.SaveChanges();
            return RedirectToAction("Index","Answers",new { PaperID= PaperID ,StuID=StuID});
        }

        public ActionResult TeAnswer(int? page)
        {
            Teacher tea = db.Teacher.Find(1);
            //获得对应的Answer
            List<Answer> answer = db.Answer.Where(p => p.TeacherID == tea.TeacherID).ToList();

            //当前页  ??:合并运算符
            int pageNumber = page ?? 1;
            //每页显示的总行数
            int pageSize = 5;
            //将我们的集合转成分页的集合
            IPagedList<Answer> answersList = answer.ToPagedList(pageNumber, pageSize);
            ViewBag.answersList = answersList;
            return View();
        }

         
        public ActionResult TeAnswerDetail()
        {
            //ViewBag.stu = db.Student.Find(id);
              
            return View();
        }


    }
}