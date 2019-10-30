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
        }


        [Route("giỏ-hàng")]
        // GET: ShoppingCart
        public ActionResult Index()
        {

            return View();
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
