using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWeibo.ViewModels;
using MyWeibo.BLL;
namespace MyWeibo.Controllers
{
    public class HomeController : Controller
    {
        //HomeBLL homeBLL = new HomeBLL();   
        public ActionResult Index()
        {
            //HomeViewModel model = new HomeViewModel();
            //model.users = homeBLL.GetHomeUserInfo(10);

            //return View(model);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
