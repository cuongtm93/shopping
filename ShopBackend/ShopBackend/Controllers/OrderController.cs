using ShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBackend.Controllers
{

    public class OrderController : Controller
    {
        private ShopBackend.Data.shop2Entities db;
        public OrderController()
        {
            db = new Data.shop2Entities();
        }
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.oc_order
                .OrderByDescending(r => r.date_added)
                .ThenByDescending(r=>r.date_modified)
                .AsQueryable();

            var items = orders.Select(r => new
            {
                order_id = r.order_id,
                customer_id = r.customer_id,
                order_status_id = r.order_status_id,
                date_added = r.date_added,
                date_modified = r.date_modified,
                total = r.total,
            }).AsQueryable();

            var model = new Order_IndexViewmodel()
            {
                Orders = new List<Order>()
            };
            foreach (var item in items)
            {
                var customer = db.oc_customer.Find(item.customer_id);
                var order_status = db.oc_order_status.Find(item.order_status_id, 1);
                var customer_fullname = customer.firstname + customer.lastname;
                model.Orders.Add(new Order()
                {
                    customer_fullname = customer_fullname,
                    date_added = item.date_added,
                    date_modified = item.date_modified,
                    order_id = item.order_id,
                    total = (int) item.total,
                    order_status = order_status.name
                });
            }
            return View(model);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var order = db.oc_order.SingleOrDefault(r => r.order_id == id);
            db.oc_order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Order/Delete/5
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

        ~OrderController()
        {
            db.Dispose();
        }
    }
}
