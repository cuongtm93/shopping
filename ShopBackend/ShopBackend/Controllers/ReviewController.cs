using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gobln.Pager;
using ShopBackend.Data;
using ShopBackend.Models;

namespace ShopBackend.Controllers
{
    public class ReviewController : Controller
    {
        const int PAGE_SIZE = 5;
        private shop2Entities db;
        public ReviewController()
        {
            db = new Data.shop2Entities();
        }

        public ActionResult Index(int? page)
        {
            page = page.HasValue ? page : 1;
            var model = from review in db.oc_review
                        join product_description in db.oc_product_description
                        on review.product_id equals product_description.product_id
                        select new Review_IndexViewmodel
                        {
                            author = review.author,
                            customer_id = review.customer_id,
                            date_added = review.date_added,
                            date_modified = review.date_modified,
                            product_name = product_description.name,
                            product_id = review.product_id,
                            rating = review.rating,
                            status = review.status,
                            text = review.text
                        };

            return View(model.ToPagedList(PAGE_SIZE).GetPage(page.Value));
        }
    }
}