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

        [HttpPost]
        public ActionResult Add_Order_History(Data.oc_order_history model)
        {
            model.date_added = DateTime.Now;
            db.oc_order_history.Add(model);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.order_id });
        }
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.oc_order
                .OrderByDescending(r => r.date_added)
                .ThenByDescending(r => r.date_modified)
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
                    total = (int)item.total,
                    order_status = order_status.name
                });
            }
            return View(model);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            var order_products = db.oc_order_product.Where(r => r.order_id == id).ToList();
            var order_products_total = order_products.Sum(r => r.price * r.quantity);
            var order_history_records = from history_record in db.oc_order_history
                                        join order_status in db.oc_order_status
                                        on history_record.order_status_id equals order_status.order_status_id
                                        select new Order_History()
                                        {
                                            comment = history_record.comment,
                                            customer_notified = (history_record.notify == 1),
                                            date_added = history_record.date_added,
                                            status = order_status.name
                                        };

            var model = db.Order_Details(id).Select(r => new Order_DetailsViewmodel()
            {
                date_added = r.date_added,
                email = r.email,
                full_name = r.fullname,
                order_id = r.order_id,

                payment_address1 = r.payment_address_1,
                payment_address2 = r.payment_address_2,
                payment_method = r.payment_method,
                payment_city = r.payment_city,
                payment_country = r.payment_country,
                shipping_address1 = r.shipping_address_1,
                shipping_address2 = r.shipping_address_2,
                shipping_city = r.shipping_city,
                shipping_country = r.shipping_country,
                shipping_method = r.shipping_method,
                store_name = r.store_name,
                telephone = r.telephone,
                total = (int)r.total,
                order_products_total = (int)order_products_total,
                order_products = order_products,
                order_history_records = order_history_records.ToList(),
                order_statuses  = db.oc_order_status.ToList()
            }).SingleOrDefault();

            return View(model);
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
