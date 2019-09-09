using Gobln.Pager;
using ShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBackend.Controllers
{
    public class ReturnController : Controller
    {
        private ShopBackend.Data.shop2Entities db;
        public ReturnController()
        {
            db = new Data.shop2Entities();
        }
        // GET: Return
        public ActionResult Index(int? page = 1)
        {
            const int PAGE_SIZE = 5;
            var model = from _return in db.oc_return
                          join _return_status in db.oc_return_status on _return.return_status_id equals _return_status.return_status_id
                          join _return_reason in db.oc_return_reason on _return.return_reason_id equals _return_reason.return_reason_id
                          join _return_action in db.oc_return_action on _return.return_action_id equals _return_action.return_action_id
                          select new Return_IndexViewmodel()
                          {
                              return_id = _return.return_id,
                              order_id = _return.order_id,
                              comment = _return.comment,
                              product = _return.product,
                              firstname = _return.firstname,
                              lastname = _return.lastname,
                              customer_id = _return.customer_id,
                              date_added = _return.date_added,
                              date_modified = _return.date_modified,
                              date_ordered = _return.date_ordered,
                              email = _return.email,
                              model = _return.model,
                              opened = _return.opened,
                              product_id = _return.product_id,
                              quantity = _return.quantity,
                              return_action = _return_action.name,
                              return_action_id = _return.return_action_id,
                              return_reason = _return_reason.name,
                              return_reason_id = _return.return_reason_id,
                              return_status = _return_status.name,
                              return_status_id = _return.return_status_id,
                              telephone = _return.telephone
                          };
            return View(model.ToPagedList(PAGE_SIZE).GetPage(page.Value));
        }

        // GET: Return/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Return/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Return/Create
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

        // GET: Return/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Return/Edit/5
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

        // GET: Return/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Return/Delete/5
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
