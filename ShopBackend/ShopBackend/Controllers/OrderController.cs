using Gobln.Pager;
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
        const int PAGE_SIZE = 5;
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

        public ActionResult Print_Shipping(int id)
        {
            var model = db.Order_Details(id).Select(r => new Order_PrintShippingViewmodel()
            {
                customer_email = r.email,
                customer_full_name = r.fullname,
                customer_telephone = r.telephone,
                date_added = r.date_added,
                invoice_prefix = r.invoice_prefix,
                order_id = r.order_id,
                payment_address1 = r.payment_address_1,
                payment_address2 = r.payment_address_2,
                payment_city = r.payment_city,
                payment_country = r.payment_country,
                payment_method = r.payment_method,
                shipping_address1 = r.shipping_address_1,
                shipping_address2 = r.shipping_address_2,
                shipping_city = r.shipping_city,
                shipping_country = r.shipping_country,
                shipping_method = r.shipping_method,
                store_id = r.store_id
            }).SingleOrDefault();

            var store = db.oc_store.Single(r => r.store_id == model.store_id);

            model.store_name = store.name;
            model.store_address = store.address;
            model.store_phone = store.phone;
            model.store_email = store.email;
            model.store_website = store.url;
            var products_without_weight_unit = from product in db.oc_product
                                               join order_product in db.oc_order_product
                                               on product.product_id equals order_product.product_id
                                               where order_product.order_id == id
                                               select new
                                               {
                                                   product.location,
                                                   order_product.name,
                                                   product.weight,
                                                   product.weight_class_id,
                                                   product.model,
                                                   order_product.quantity
                                               };

            var products = from product in products_without_weight_unit
                           join weight_class_description in db.oc_weight_class_description
                           on product.weight_class_id equals weight_class_description.weight_class_id
                           select new Order_PrintShipping_Products()
                           {
                               location = product.location,
                               model = product.model,
                               name = product.name,
                               quantity = product.quantity,
                               weight = product.weight,
                               weight_unit = weight_class_description.unit
                           };

            model.Products = products.ToList();

            return View(model);
        }
        public ActionResult Print_Invoice(int id)
        {
            var order_products = db.oc_order_product.Where(r => r.order_id == id).ToList();
            var order_products_total = order_products.Sum(r => r.price * r.quantity);

            var model = db.Order_Details(id).Select(r => new Order_PrintInvoiceViewmodel()
            {
                date_added = r.date_added,
                invoice_prefix = r.invoice_prefix,
                email = r.email,
                full_name = r.fullname,
                order_id = r.order_id,
                store_id = r.store_id,
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
            }).SingleOrDefault();

            var store = db.oc_store.SingleOrDefault(r => r.store_id == model.store_id);
            model.store_address = store.address;
            model.store_phone = store.phone;
            model.store_email = store.email;
            model.store_website = store.url;
            return View(model);
        }
        // GET: Order
        public ActionResult Index(int? page)
        {
            if (!page.HasValue) page = 1;

            var model = from order in db.oc_order
                       join customer in db.oc_customer on order.customer_id equals customer.customer_id
                       join status in db.oc_order_status on order.order_status_id equals status.order_status_id
                       orderby order.date_added , order.date_modified
                       select new Order_IndexViewmodel
                       {
                           customer_fullname = customer.firstname + customer.lastname,
                           date_added= order.date_added,
                           date_modified= order.date_modified,
                           total = order.total,
                           order_id = order.order_id,
                           order_status = status.name
                       };

            return View(model.ToPagedList(PAGE_SIZE).GetPage(page.Value));
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
                order_statuses = db.oc_order_status.ToList()
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
