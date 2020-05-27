using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
namespace UserRoles.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        AccountDBEntities db = new AccountDBEntities();
        // GET: Menus
        public ActionResult Index(string Name="")
        {
            List<Menus> list = db.Menus.Where(p=>Name==""||p.Name.Contains(Name)).ToList();
            ViewBag.Name = Name;
            return View(list);
        }
        //public ActionResult Index(int? page)
        //{
        //    List<Menus> list = db.Menus.OrderByDescending(p => p.ID).ToList();
        //    int pageNumber = page ?? 1;
        //    //每页显示多少条  
        //    int pageSize = 10;
        //    //通过ToPagedList扩展方法进行分页  
        //    IPagedList<Menus> MenusPagedList = list.ToPagedList(pageNumber, pageSize);

        //    return View(MenusPagedList);
        //}
        

        public ActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Add(Menus menus)
        {
            db.Menus.Add(menus);
            db.SaveChanges();
            return RedirectToAction("Index","Menu");
        }
        public ActionResult Edit(int id)
        {
            Menus menus = db.Menus.Find(id);

            return View(menus);
        }
        [HttpPost]
        public ActionResult Edit(Menus menus)
        {
            db.Entry(menus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(menus);
        }
        //public ActionResult Index()
        //{
        //    List<Menus> list = db.Menus.ToList();
        //    return View(list);
        //}

        //public ActionResult Add()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Add(Menus menus)
        //{
        //    db.Menus.Add(menus);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult MenuUpdate(int ID)
        {
            Menus menu = db.Menus.Find(ID);
            ViewBag.menu = menu;

            db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult MenuUpdate(Menus menus)
        {
            db.Entry(menus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int id)
        //{
        //    List<R_Role_Menus> rlist = db.R_Role_Menus.Where(p=>p.MenuID==id).ToList();
        //    if (rlist.Count>0)
        //    {
        //        return Content("<script>alert('请解除该用户的角色绑定，再删除该用户！'):history.go(-1)</script>");
        //    }
        //    Menus menus = db.Menus.Find(id);
        //    db.Menus.Remove(menus);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult MenuDelete(int MenuID)
        {
            var RoleMenus = db.R_Role_Menus.Where(p => p.MenuID == MenuID).ToList();
            if (RoleMenus.Count > 0)
            {
                return Content("<script>alert('请解除该用户的角色绑定，再删除该用户！');history.go(-1);</script>");
            }
            else
            {
                var menulist = db.Menus.Find(MenuID);
                db.Menus.Remove(menulist);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }


}

