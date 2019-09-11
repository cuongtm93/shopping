using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopFrontend.App_Start;
using ShopFrontend.Models;

namespace ShopFrontend.Controllers
{
    public class ProductController : Controller
    {
        private shop2Entities db;
        public ProductController()
        {
            db = new shop2Entities();
        }
        // GET: Product
        public ActionResult Index(string seo_url)
        {
            var product_desc = db.oc_product_description.SingleOrDefault(r => r.seo_url == seo_url);
            var product_id = product_desc.product_id;
            var product = db.oc_product.Find(product_id);
            var product_alternative_images = db.oc_product_image.Where(r => r.product_id == product_id).OrderBy(r => r.sort_order);
            var model = new ProductIndexViewmodel()
            {
                product = product,
                product_description = product_desc,
                product_alternative_images = product_alternative_images
            };
            return View(model);
        }
    }
}