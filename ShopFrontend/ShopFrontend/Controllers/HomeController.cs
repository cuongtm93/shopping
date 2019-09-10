using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopFrontend.Models;
namespace ShopFrontend.Controllers
{
    public class HomeController : Controller
    {
        private shop2Entities db;
        public HomeController()
        {
            db = new shop2Entities();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}