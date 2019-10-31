using ShopFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ShopFrontend.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private shop2Entities db = new shop2Entities();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (User.Identity.IsAuthenticated)
            {
                if (!db.oc_customer_online.Any(r => r.ip == Request.UserHostAddress))
                {
                    db.oc_customer_online.Add(new oc_customer_online
                    {
                        ip = Request.UserHostAddress,
                        date_added = DateTime.Now,
                        referer = Request.UrlReferrer.ToString() ?? "",
                        url = Request.Url.ToString(),
                        customer_id = db.oc_customer.Where(r => r.email == User.Identity.Name).Single().customer_id
                    });
                }
                else
                {
                    var record = db.oc_customer_online.Where(r => r.ip == Request.UserHostAddress).Single();
                    record.referer = Request.UrlReferrer == null ? "" : Request.UrlReferrer.ToString();
                    record.url = Request.Url.ToString();
                }
                db.SaveChanges();
            }
        }
        [Route("giỏ-hàng")]
        // GET: ShoppingCart
        public ActionResult Index()
        {
            int customer_id = db.oc_customer.Where(r => r.email == this.User.Identity.Name).Single().customer_id;
            var list = from cart in db.oc_cart
                       join product in db.oc_product on cart.product_id equals product.product_id
                       join product_des in db.oc_product_description on product.product_id equals product_des.product_id
                       where cart.customer_id == customer_id
                       select new
                       {
                           product.product_id,
                           product_name = product_des.name,
                           product_price = product.price,
                           cart.quantity
                       };

            var model = new ShoppingCart_IndexViewmodel()
            {
                List = list.Select(r => new ShoppingCart_Index
                {
                    product_id = r.product_id,
                    product_name = r.product_name,
                    quantity = r.quantity,
                    product_price = r.product_price
                }).ToList()
            };
            model.customer_address = db.oc_address.Where(r => r.customer_id == customer_id).SingleOrDefault();
            model.total = model.List.Sum(r => r.product_price * r.quantity);
            return View(model);
        }

        public int Cart_Count()
        {
            int customer_id = db.oc_customer.Where(r => r.email == this.User.Identity.Name).Single().customer_id;
            var list = from cart in db.oc_cart
                       join product in db.oc_product on cart.product_id equals product.product_id
                       join product_des in db.oc_product_description on product.product_id equals product_des.product_id
                       where cart.customer_id == customer_id
                       select new
                       {
                           product.product_id,
                           product_name = product_des.name,
                           product_price = product.price,
                           cart.quantity
                       };
            return list.Count();
        }
        public ActionResult Clear()
        {
            int customer_id = db.oc_customer.Where(r => r.email == this.User.Identity.Name).Single().customer_id;
            var delete_items = db.oc_cart.Where(r => r.customer_id == customer_id).ToList();
            db.oc_cart.RemoveRange(delete_items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int product_id)
        {
            int customer_id = db.oc_customer.Where(r => r.email == this.User.Identity.Name).Single().customer_id;
            var items = db.oc_cart.Where(r => r.customer_id == customer_id && r.product_id == product_id);
            db.oc_cart.RemoveRange(items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Create(int product_id, int quantity)
        {
            int customer_id = db.oc_customer.Where(r => r.email == this.User.Identity.Name).Single().customer_id;
            var product_is_already_in_cart = db.oc_cart.Any(r => r.customer_id == customer_id && r.product_id == product_id);
            if (product_is_already_in_cart)
            {
                var cart_item = db.oc_cart.Single(r => r.customer_id == customer_id && r.product_id == product_id);
                cart_item.quantity += quantity;
                db.SaveChanges();
                return Json(new
                {
                    m = $"Đã thêm sp {product_id} , số lượng {quantity}"
                });
            }

            // product not in cart
            db.oc_cart.Add(new oc_cart()
            {
                api_id = 0,
                customer_id = customer_id,
                product_id = product_id,
                date_added = DateTime.Now,
                quantity = quantity,
                option = string.Empty,
                recurring_id = 0,
                session_id = this.Session.SessionID,

            });
            db.SaveChanges();
            return Json(new
            {
                m = $"Đã thêm sp {product_id} , số lượng {quantity}"
            });
        }
    }
}
