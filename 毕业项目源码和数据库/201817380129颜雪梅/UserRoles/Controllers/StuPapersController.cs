using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
namespace UserRoles.Controllers
{
    public class StuPapersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: StuPapers
        public ActionResult Index()
        {
            List<Paper> p = db.Paper.ToList();
            return View(p);
        }
    }
}