using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
using PagedList;
using UserRoles.Filter;
namespace Account.Controllers
{
    [Login]
    public class RoleController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Roles
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult Index(int? page, string Name = "")
        {
            List<Roles> list = db.Roles.Where(p => Name == "" || p.Name.Contains(Name)).ToList();
            ViewBag.Name = Name;
            ViewData["list"] = list;
            int pageNumber = page ?? 1;
            //每页显示多少条  
            int pageSize = 5;
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Roles> RolesPagedList = list.ToPagedList(pageNumber, pageSize);

            return View(RolesPagedList);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Roles roles)
        {
            db.Roles.Add(roles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Delete(int ID)
        {
            List<R_Role_Menus> rlist = db.R_Role_Menus.Where(p => p.RoleID == ID).ToList();
            var rur=db.R_User_Roles.Where(p => p.RoleID == ID).ToList();
            if (rlist.Count > 0)
            {
                return Content("<script>alert('请解除该角色的菜单绑定，再删除该角色！');history.go(-1)</script>");
            }
            else if (rur.Count > 0)
            {
                return Content("<script>alert('撤除用户绑定才可以删除角色！');history.go(-1)</script>");
            }
            else {
                Roles roles = db.Roles.Find(ID);
                db.Roles.Remove(roles);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 删除指定菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult DelMenu(int? RMenuID)
        {
            R_Role_Menus rrm = db.R_Role_Menus.Find(RMenuID);
            db.R_Role_Menus.Remove(rrm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult SetMenu(int RoleID)
        {
            //通过用户id查到用户信息,并存储ViewBag中
            Roles roles = db.Roles.Find(RoleID);
            ViewBag.roles = roles;
            //通过用户id查到用户对应得角色关系，并存储ViewBag中
            List<R_Role_Menus> rRMList = db.R_Role_Menus.Where(p => p.RoleID == RoleID).ToList();
            ViewBag.rRMList = rRMList;
            //获得所有得角色，并存储ViewBag中
            List<Menus> MenusList = db.Menus.ToList();
            ViewBag.MenusList = MenusList;
            return View();
        }

        [HttpPost]
        public ActionResult SetMenu(int RoleID, List<int> menus)
        {
            //先通过用户ID去清除所有与该用户相关得角色关系
            var rRMList = db.R_Role_Menus.Where(p => p.RoleID == RoleID);
            foreach (var rRM in rRMList)
            {
                db.R_Role_Menus.Remove(rRM);
            }
            db.SaveChanges();

            if (menus != null)
            {
                //添加新的角色关系
                foreach (var roleID in menus)
                {
                    //角色id和用户id组成新的R_User_Roles对象，添加到上下文对应R_User_Roles的集合中
                    R_Role_Menus rrm = new R_Role_Menus()
                    {
                        RoleID = RoleID,
                        MenuID = roleID
                    };
                    db.R_Role_Menus.Add(rrm);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
