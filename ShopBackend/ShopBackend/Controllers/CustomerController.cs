using Gobln.Pager;
using ShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBackend.Controllers
{
    public class CustomerController : Controller
    {
        private ShopBackend.Data.shop2Entities db;
        public CustomerController()
        {
            db = new Data.shop2Entities();
        }
        // GET: Customer
        public ActionResult Index(int? page = 1)
        {
            const int PAGE_SIZE = 5;
            var model = from _customer in db.oc_customer
                        join _customer_group_description in db.oc_customer_group_description on _customer.customer_group_id equals _customer_group_description.customer_group_id
                        select new Customer_IndexViewmodel
                        {
                            customer_group = _customer_group_description.name,
                            customer_group_id = _customer.customer_group_id,
                            customer_id = _customer.customer_id,
                            date_added = _customer.date_added,
                            email = _customer.email,
                            firstname = _customer.firstname,
                            lastname = _customer.lastname,
                            ip = _customer.ip,
                            status = _customer.status,
                            store_id = _customer.store_id,
                            telephone = _customer.telephone
                        };
        return View(model.ToPagedList(PAGE_SIZE).GetPage(page.Value));
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
