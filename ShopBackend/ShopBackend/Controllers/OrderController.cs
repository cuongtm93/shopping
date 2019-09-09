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
            if (string.IsNullOrWhiteSpace(model.comment))
            {
                model.comment = "Không ghi chú";
            }
            db.oc_order_history.Add(model);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.order_id });
        }

        public ActionResult Print_Shipping(int id)
        {
            var _ = from order in db.oc_order
                    join customer in db.oc_customer on order.customer_id equals customer.customer_id
                    join _store in db.oc_store on order.store_id equals _store.store_id
                    where order.order_id == id
                    select new Order_PrintShippingViewmodel
                    {
                        customer_email = customer.email,
                        customer_full_name = customer.firstname + customer.lastname,
                        customer_telephone = customer.telephone,
                        date_added = order.date_added,
                        invoice_prefix = order.invoice_prefix,
                        order_id = order.order_id,
                        payment_address1 = order.payment_address_1,
                        payment_address2 = order.payment_address_2,
                        payment_city = order.payment_city,
                        payment_country = order.payment_country,
                        payment_method = order.payment_method,
                        shipping_address1 = order.shipping_address_1,
                        shipping_address2 = order.shipping_address_2,
                        shipping_city = order.shipping_city,
                        shipping_country = order.shipping_country,
                        shipping_method = order.shipping_method,
                        store_id = order.store_id,
                        store_address = _store.address,
                        store_email = _store.email,
                        store_name = _store.name,
                        store_phone = _store.phone,
                        store_website = _store.url
                    };

            var products = from product in db.oc_product
                           join product_details in db.oc_product_description on product.product_id equals product_details.product_id
                           join order_product in db.oc_order_product on product.product_id equals order_product.product_id
                           join _unit in db.oc_weight_class_description on product.weight_class_id equals _unit.weight_class_id
                           where order_product.order_id == id
                           select new Order_PrintShipping_Products()
                           {
                               location = product.location,
                               model = product.model,
                               name = product_details.name,
                               quantity = order_product.quantity,
                               weight = product.weight,
                               weight_unit = _unit.unit
                           };
            var model = _.FirstOrDefault();
            model.Products = products.ToList();
            return View(model);
        }
        public ActionResult Print_Invoice(int id)
        {
            var order_products = db.oc_order_product.Where(r => r.order_id == id).ToList();
            var order_products_total = order_products.Sum(r => r.price * r.quantity);
            var details = from order in db.oc_order
                          join customer in db.oc_customer on order.customer_id equals customer.customer_id
                          join _store in db.oc_store on order.store_id equals _store.store_id
                          where order.order_id == id
                          select new Order_PrintInvoiceViewmodel
                          {
                              date_added = order.date_added,
                              invoice_prefix = order.invoice_prefix,
                              email = _store.email,
                              full_name = customer.firstname + customer.lastname,
                              order_id = order.order_id,
                              store_id = _store.store_id,
                              payment_address1 = order.payment_address_1,
                              payment_address2 = order.payment_address_2,
                              payment_method = order.payment_method,
                              payment_city = order.payment_city,
                              payment_country = order.payment_country,
                              shipping_address1 = order.shipping_address_1,
                              shipping_address2 = order.shipping_address_2,
                              shipping_city = order.shipping_city,
                              shipping_country = order.shipping_country,
                              shipping_method = order.shipping_method,
                              store_name = _store.name,
                              telephone = _store.phone,
                              total = (int)order.total,
                              store_address = _store.address,
                              store_email = _store.email,
                              store_phone = _store.phone,
                              store_website = _store.url,
                              order_products_total = order_products_total,
                          };

            var model = details.FirstOrDefault();
            model.order_products = order_products;
            return View(model);
        }
        // GET: Order
        public ActionResult Index(int? page)
        {
            if (!page.HasValue) page = 1;

            var model = from order in db.oc_order
                        join customer in db.oc_customer on order.customer_id equals customer.customer_id
                        join status in db.oc_order_status on order.order_status_id equals status.order_status_id
                        orderby order.date_added, order.date_modified
                        select new Order_IndexViewmodel
                        {
                            customer_fullname = customer.firstname + customer.lastname,
                            date_added = order.date_added,
                            date_modified = order.date_modified,
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

            var details = from order in db.oc_order
                          join customer in db.oc_customer on order.customer_id equals customer.customer_id
                          join _store in db.oc_store on order.store_id equals _store.store_id
                          where order.order_id == id
                          select new Order_DetailsViewmodel
                          {
                              date_added = order.date_added,
                              email = _store.email,
                              full_name = customer.firstname + customer.lastname,
                              order_id = order.order_id,
                              payment_address1 = order.payment_address_1,
                              payment_address2 = order.payment_address_2,
                              payment_method = order.payment_method,
                              payment_city = order.payment_city,
                              payment_country = order.payment_country,
                              shipping_address1 = order.shipping_address_1,
                              shipping_address2 = order.shipping_address_2,
                              shipping_city = order.shipping_city,
                              shipping_country = order.shipping_country,
                              shipping_method = order.shipping_method,
                              store_name = _store.name,
                              telephone = _store.phone,
                              total = (int)order.total,
                          };

            var model = details.FirstOrDefault();
            model.order_products = order_products;
            model.order_history_records = order_history_records.ToList();
            model.order_statuses = db.oc_order_status.ToList();
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
