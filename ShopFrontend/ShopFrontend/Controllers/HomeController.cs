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
                                           seo_url = product_description.seo_url,
                                           product_id = product.product_id,
                                           date_added = product.date_added,
                                           date_available = product.date_available,
                                           date_modified = product.date_modified,
                                           deleted = product.deleted,
                                           model = product.model,
                                           quantity = product.quantity,
                                           status = product.status,
                                           points = product.points,
                                           name = product_description.name,
                                           price = product.price,
                                           image = product.image,
                                           stock_status = stock_status.name,
                                       }).Take(4).ToList();

            top_4_news_products.ForEach((product) =>
            {
                product.alternative_images = db.oc_product_image.Where(r => r.product_id == product.product_id).ToList();
            });

            var toplevel_categories = from category in db.oc_category
                                      join category_description in db.oc_category_description on category.category_id equals category_description.category_id
                                      where category.parent_id == 0
                                      select new
                                      {
                                          category_description
                                      };

            var model = new Home_IndexViewmodel()
            {
                top_4_maybeyoulike_products = top_4_news_products.Take(4),
                top_4_news_products = top_4_news_products.Take(4),
                toplevel_categories = toplevel_categories.Select(r => r.category_description).ToList()
            };

            return View(model);
        }

        [Route("danh-mục/{seo_url}")]
        public ActionResult Category(string seo_url)
        {
            var category_id = db.oc_category_description.Where(r => r.seo_url == seo_url).SingleOrDefault()?.category_id;
            if (category_id.HasValue == false)
            {
                throw new Exception($"category {seo_url} không tồn tại");
            }
            var products = (from product in db.oc_product
                            join prod_to_category in db.oc_product_to_category on product.product_id equals prod_to_category.product_id
                            join prod_des in db.oc_product_description on product.product_id equals prod_des.product_id
                            join stock_status in db.oc_stock_status on product.stock_status_id equals stock_status.stock_status_id
                            where product.deleted != 1 && prod_to_category.category_id == category_id.Value
                            select new Product()
                            {
                                price = product.price,
                                product_id = product.product_id,
                                date_available = product.date_available,
                                date_added = product.date_added,
                                date_modified = product.date_modified,
                                model = product.model,
                                stock_status = stock_status.name,
                                image = product.image,
                                name = prod_des.name,
                                seo_url = prod_des.seo_url
                            }).ToList();

            products.ForEach(prod =>
            {
                prod.alternative_images = db.oc_product_image.Where(r => r.product_id == prod.product_id);
            });
            var toplevel_categories = from category in db.oc_category
                                      join category_description in db.oc_category_description on category.category_id equals category_description.category_id
                                      where category.parent_id == 0
                                      select new
                                      {
                                          category_description
                                      };
            var model = new Home_CategoryViewmodel()
            {
                products = products,
                toplevel_categories = toplevel_categories.Select(r=>r.category_description)
            };
            return View(model);
        }


    }
}