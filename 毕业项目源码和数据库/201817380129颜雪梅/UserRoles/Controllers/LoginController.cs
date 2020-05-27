using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
namespace UserRoles.Controllers
{
    public class LoginController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}