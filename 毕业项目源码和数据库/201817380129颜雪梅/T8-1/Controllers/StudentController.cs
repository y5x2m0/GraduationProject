using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T8_1.Models;
namespace T8_1.Controllers
{
    public class StudentController : Controller
    {
        AccountDBEntities1 db = new AccountDBEntities1();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Show()
        {
            return PartialView(db.Student.ToList());
        }
    }
}