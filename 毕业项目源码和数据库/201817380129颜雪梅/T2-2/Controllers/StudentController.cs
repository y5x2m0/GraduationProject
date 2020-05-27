using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2_2.Models;
namespace T2_2.Controllers
{
    public class StudentController : Controller
    {
        ExamDBEntities db = new ExamDBEntities();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.TeaID = id;
            return View();
        }

    }
}