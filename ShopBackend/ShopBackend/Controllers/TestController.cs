using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBackend.App_Start;
namespace ShopBackend.Controllers
{
    public class TestController : Controller
    {
        private ShopBackend.Data.shop2Entities db;
        public TestController()
        {
            db = new Data.shop2Entities();
        }
        // GET: Test
        public ActionResult Index()
        {
            var ds = db.oc_product_description.ToList();
            foreach (var item in ds)
            {
                item.seo_url = item.name.UrlFriendly();
            }
            db.SaveChanges();
            return View();
        }
    }
}