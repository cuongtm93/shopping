using ShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBackend.Controllers
{
    public class StatisticController : Controller
    {
        private ShopBackend.Data.shop2Entities db;
        public StatisticController()
        {
            db = new Data.shop2Entities();
        }
        // GET: Statistic
        public ActionResult Index()
        {
            var model = new Statistic_IndexViewmodel()
            {
                statistics = db.oc_statistics.ToList()
            }; 
            return View(model);
        }
        public ActionResult Update()
        {
            var order_sale = db.oc_statistics.SingleOrDefault(r => r.code == "order_sale");
            var order_processing = db.oc_statistics.SingleOrDefault(r => r.code == "order_processing");
            var order_complete = db.oc_statistics.SingleOrDefault(r => r.code == "order_complete");
            var order_other = db.oc_statistics.SingleOrDefault(r => r.code == "order_other");
            var returns = db.oc_statistics.SingleOrDefault(r => r.code == "returns");
            var product = db.oc_statistics.SingleOrDefault(r => r.code == "product");
            var review = db.oc_statistics.SingleOrDefault(r => r.code == "review");
            const int orderstatus_completed = 5;
            const int orderstatus_processing = 2;
            const int returnstatus_complete = 2;
            order_sale.value = db.Database.SqlQuery<decimal>("select sum(oc_order.total) from shop.oc_order").SingleOrDefault();
            order_complete.value = db.Database.SqlQuery<int>($"select count(*) from shop.oc_order where oc_order.order_status_id = {orderstatus_completed}").SingleOrDefault();
            order_processing.value = db.Database.SqlQuery<int>($"select count(*) from shop.oc_order where oc_order.order_status_id = {orderstatus_processing}").SingleOrDefault();
            returns.value = db.Database.SqlQuery<int>($"select count(*) from shop.oc_return where oc_return.return_status_id = {returnstatus_complete}").SingleOrDefault();
            product.value = db.Database.SqlQuery<int>($"select count(*) from shop.oc_product").SingleOrDefault();
            review.value = db.Database.SqlQuery<int>($"select count(*) from shop.oc_review").SingleOrDefault();
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Statistic/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Statistic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistic/Create
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

        // GET: Statistic/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Statistic/Edit/5
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

        // GET: Statistic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Statistic/Delete/5
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
