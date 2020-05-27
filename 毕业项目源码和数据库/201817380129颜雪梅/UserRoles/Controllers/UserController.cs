using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
using System.IO;
using System.Collections;

namespace UserRoles.Controllers
{
    public class UserController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: User
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="departmentID">部门查询</param>
        /// <param name="Name">用户名模糊查询</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页显示的行数</param>
        /// <returns></returns>
        public ActionResult Index(int departmentID = 0, string Name = "", int pageIndex = 1, int pageCount = 5)
        {
            //获得部门下拉框列表
            List<Departments> dList = db.Departments.ToList();
            ViewBag.dList = dList;
            //获得根据条件查询的总行数

            int tatalCount = db.UserInfos.OrderBy(p => p.ID)
                .Where(p => (departmentID == 0 || p.DepartmentID == departmentID) && ((Name == "") || p.Name.Contains(Name)))
                .Count();
            //获得总页数

            double tataoPage = Math.Ceiling(tatalCount / (double)pageCount);
            //获得用户表,先按照主键正序查询Skip()跳过指定数量的元素，返回剩下的集合.Take()从剩下集合数中，从第一条获取指定数量的集合
            List<UserInfos> list = db.UserInfos.OrderBy(p => p.ID)
                .Where(p => (departmentID == 0 || p.DepartmentID == departmentID) && (Name == "" || p.Name.Contains(Name)))
                .Skip((pageIndex - 1) * pageCount).Take(pageCount)
                .ToList();
            ;

            //列表加载的同时，将条件存储到对应的控件显示
            ViewBag.departmentID = departmentID;
            ViewBag.name = Name;
            //当前页
            ViewBag.pageIndex = pageIndex;
            //每页行数
            ViewBag.pageCount = pageCount;
            //总行数
            ViewBag.tatalCount = tatalCount;
            //总页数
            ViewBag.tataoPage = tataoPage;
            return View(list);

        }
        //public ActionResult Add()
        //{
        //    List<Departments> list = db.Departments.ToList();
        //    ViewBag.Roles = db.Roles.ToList();
        //    return View(list);
        //}

        //[HttpPost]
        //public ActionResult Add(int[] RoleID, UserInfo user)
        //{
        //    db.UserInfos.Add(user);
        //    db.SaveChanges();
        //    foreach (var item in RoleID)
        //    {
        //        R_User_Roles rur = new R_User_Roles()
        //        {
        //            RoleID = item,
        //            UserID = user.ID
        //        };
        //        db.R_User_Roles.Add(rur);
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index","User");
        //}
        /// <summary>
        /// 跳转到添加用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            //获得所有的部门
            ViewBag.Departments = db.Departments.ToList();
            //获得所有的角色
            ViewBag.Roles = db.Roles.ToList();
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="Photo">获得上传的图片</param>
        /// <param name="roles">获得选中的角色</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(UserInfos user, HttpPostedFileBase Photo, int[] roles)
        {
            //处理图片
            if (Photo != null)
            {
                //1、获得文件的名称
                string fileName = Path.GetFileName(Photo.FileName);

                //2、判断文件是否是图片
                string hz = Path.GetExtension(fileName);//.jpg
                if (fileName.EndsWith("jpg") || fileName.EndsWith("png") || fileName.EndsWith("gif") || fileName.EndsWith("jpeg"))
                {
                    //3、保存图片到项目文件夹中
                    Photo.SaveAs(Server.MapPath("~/images/users/" + fileName));
                    //4、将图片文件名，绑定到该用户的Photo字段中
                    user.Photo = fileName;
                }
                else
                {
                    return Content("<script>alert('上传的文件必须是图片！')</script>");
                }
            }
            else
            {
                return Content("<script>alert('未获取上传的文件')</script>");
            }

            //添加用户
            db.UserInfos.Add(user);
            db.SaveChanges();
            //添加用户与角色的关系
            foreach (var roleID in roles)
            {
                R_User_Roles rur = new R_User_Roles();
                rur.RoleID = roleID;
                rur.UserID = user.ID;
                db.R_User_Roles.Add(rur);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="UserID">获取用户的id</param>
        /// <returns></returns>
        public ActionResult Edit(int UserID)
        {
            //获得用户信息
            ViewBag.User = db.UserInfos.Find(UserID);

            //获得所有的部门
            ViewBag.Departments = db.Departments.ToList();

            //获得所有的角色
            ViewBag.Roles = db.Roles.ToList();

            //获得用户绑定的角色关系列表
            ViewBag.Rur = db.R_User_Roles.Where(p => p.UserID == UserID).ToList();
            return View();


        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="EPhoto"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserInfos user, HttpPostedFileBase EPhoto, int[] roles)
        {
            //处理图片
            //1、如果EPhoto不为空就处理图片，替换user里的原来的图片，否则就不处理图片保留原来的图片
            if (EPhoto != null)
            {
                //1.获得文件的名称
                string fileName = Path.GetFileName(EPhoto.FileName);

                //2.判断文件是否为图片
                //string hz = Path.GetExtension(fileName);//获得文件后缀名.jpg
                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png") || fileName.EndsWith(".gif") || fileName.EndsWith(".jpeg"))
                {
                    //3.保存图片到项目文件夹中
                    EPhoto.SaveAs(Server.MapPath("~/images/users" + fileName));
                    //4.将图片文件名，绑定到该用户的photo字段中
                    user.Photo = fileName;
                }
            }
            //2、修改用户信息，该用户的各项属性的值已标记为已被修改，则全部进行修改
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            //保存到数据库中
            db.SaveChanges();

            //3、修改用户与角色的关系
            var rURList = db.R_User_Roles.Where(p => p.UserID == user.ID);
            foreach (var rUR in rURList)
            {
                db.R_User_Roles.Remove(rUR);
            }
            db.SaveChanges();
            //添加新的角色关系
            foreach (var roleID in roles)
            {
                //角色id和用户id组成新的R_User_Roles对象，添加到上下文对应R_User_Roles的集合中
                R_User_Roles rur = new R_User_Roles()
                {
                    UserID = user.ID,
                    RoleID = roleID
                };
                db.R_User_Roles.Add(rur);
            }
            db.SaveChanges();

            return RedirectToAction("Index", "User");

        }
        public ActionResult Remove(int UserID)
        {
            var rURList = db.R_User_Roles.Where(p => p.UserID == UserID).ToList();
            if (rURList.Count > 0)
            {
                return Content("<script>alert('请解除该用户的角色绑定，再删除该用户！');history.go(-1);</script>");
            }
            else
            {
                UserInfos user = db.UserInfos.Find(UserID);
                db.UserInfos.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult deleteAll()
        {
            ArrayList arr = new ArrayList();
            string rkeyStr = "";
            StringBuilder sb = new StringBuilder();
            return View();
        }


    }
}