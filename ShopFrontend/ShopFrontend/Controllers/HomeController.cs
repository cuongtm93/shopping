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
            var enabled = 1;
            var top_4_news_products = (from product in db.oc_product
                                       join product_description in db.oc_product_description on product.product_id equals product_description.product_id
                                       join stock_status in db.oc_stock_status on product.stock_status_id equals stock_status.stock_status_id
                                       where product.deleted != 1 && product.status == enabled
                                       orderby product.date_added, product.date_available, product.date_modified
                                       select new Product()
                                       {
                                           product_id = product.product_id,
                                           date_added = product.date_added,
                                           date_available = product.date_available,
                                           date_modified = product.date_modified,
                                           deleted = product.deleted,
                                           model = product.model,
                                           quantity = product.quantity,
                                           status = product.status,
                                           points  = product.points,
                                           name = product_description.name,
                                           price = product.price,
                                           image = product.image,
                                           stock_status  = stock_status.name,
                                       }).Take(4).ToList();

            top_4_news_products.ForEach((product) =>
            {
                product.alternative_images = db.oc_product_image.Where(r => r.product_id == product.product_id).ToList();
            });

            var model = new Home_IndexViewmodel()
            {
                top_4_maybeyoulike_products = top_4_news_products.Take(4),
                top_4_news_products = top_4_news_products.Take(4),
            };
            return View(model);
        }
    }
}