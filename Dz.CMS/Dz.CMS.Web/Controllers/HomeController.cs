using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dz.CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            Dz.CMS.Services.Services.UserService userService = new Services.Services.UserService();

            var user = userService.GetModelList("UserName='王小龙'") ;

           
            ViewBag.Message ="操作成功";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }

        public ActionResult Create()
        {
             //Dz.CMS.Services.Services.UserService userService = new Services.Services.UserService();

            //var user = userService.GetModelList("UserName='王小龙'").FirstOrDefault() as Dz.CMS.Model.Models.User;
            return View(new Dz.CMS.Model.Models.User() { UserName = "王小龙", Address= "成都" , Age=20, Phone="18200465605" });
        }
    }
}
