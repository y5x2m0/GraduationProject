using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace T2_2.Controllers
{
    public class ResourceController : Controller
    {
        // GET: Resource
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="resourceFile">获取的客户端上传的单个文件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase resourceFile)
        {
            if (resourceFile != null)
            {
                if (resourceFile.ContentLength > 0 && resourceFile.ContentLength < 4194304)
                {
                    string fileName = Path.GetFileName(resourceFile.FileName);
                    //string suffix = Path.GetExtension(fileName);
                    string suffix = fileName.Substring(fileName.LastIndexOf(".")+1);
                    if (suffix == "gif" || suffix == "jpg" || suffix == "png")
                    {
                        resourceFile.SaveAs(Server.MapPath("~/UploadFile/" + fileName));
                        ViewBag.Message = "文件上传成功！";
                        ViewBag.ImgName = fileName;
                    }
                    else
                    {
                        ViewBag.Message = "文件上传格式不正确！";
                    }
                   
                }
                else
                {
                    ViewBag.Message = "大小不符合要求";
                }
            }
            else
            {
                ViewBag.Message = "请上传文件！";
            }
            return View();
        }
    }
}