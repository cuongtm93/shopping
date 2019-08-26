using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBackend.Data;
using System.Security.Cryptography;
using System.Text;

namespace ShopBackend.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly shop2Entities db;

        public HomeController()
        {
            db = new shop2Entities();
        }
       
        public ActionResult Index()
        {
            return View();
        }
    }
}