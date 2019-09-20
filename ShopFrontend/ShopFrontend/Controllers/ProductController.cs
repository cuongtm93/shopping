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
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!db.oc_customer_online.Any(r => r.ip == Request.UserHostAddress))
                {
                    db.oc_customer_online.Add(new oc_customer_online
                    {
                        ip = Request.UserHostAddress,
                        date_added = DateTime.Now,
                        referer = Request.UrlReferrer.ToString(),
                        url = Request.Url.ToString(),
                        customer_id = db.oc_customer.Where(r => r.email == User.Identity.Name).Single().customer_id
                    });
                }
                else
                {
                    var record = db.oc_customer_online.Where(r => r.ip == Request.UserHostAddress).Single();
                    record.referer = Request.UrlReferrer.ToString();
                    record.url = Request.Url.ToString();
                }
                db.SaveChanges();
            }
            base.OnActionExecuted(filterContext);
            
        }
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
                product_alternative_images = product_alternative_images,
            };

            #region Tạo thanh breadscrumb
            // targeted->parent_of_targeted ..... ->top_category
            var root_category_id = db.oc_product_to_category
                .Where(r => r.product_id == product_desc.product_id)
                .FirstOrDefault()
                .category_id;
            var fist_category_id = root_category_id;
            var category_tree = new List<oc_category_description>();
            while (true)
            {
                var next_category_id = db.oc_category.SingleOrDefault(r => r.category_id == root_category_id).parent_id;
                if (next_category_id != 0)
                {
                    if (!db.oc_category.Any(r => r.category_id == next_category_id))
                        break;

                    category_tree.Add(db.oc_category_description.SingleOrDefault(r=>r.category_id == next_category_id));
                    root_category_id = next_category_id;
                }
                else
                {
                    break;
                }
            }

            // top_category -> child_of_top -> ... -> targeted
            category_tree.Reverse();
            if (db.oc_category_description.Any(r => r.category_id == fist_category_id))
            {
                category_tree.Add(db.oc_category_description.SingleOrDefault(r => r.category_id == fist_category_id));
            }
            model.breadscrumb = category_tree; 
            #endregion
            return View(model);
        }
    }
}