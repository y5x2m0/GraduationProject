﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StuSystem.Controllers
{
    public class Test1Controller : Controller
    {
        // GET: Test1
        public ActionResult Index()
        {
            return View();
        }
            public ActionResult Login()
            {
                return View();
            }
        public ActionResult bottom()
        {
            return View();
        }
    }
}