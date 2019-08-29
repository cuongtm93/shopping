using ShopBackend.Data;
using ShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBackend.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly shop2Entities db;
        public ManufacturerController()
        {
            db = new shop2Entities();
        }
        // GET: Manufacturer
        public ActionResult Index()
        {
            var model = new Manufacturer_IndexViewmodel()
            {
                Manufacturers = db.oc_manufacturer
                .Where(r => r.status == 1).ToList()
                .Select(r => new Manufacturer()
                {
                    Mame = r.name,
                    Manufacturer_id = r.manufacturer_id,
                    Sort_order = r.sort_order
                }).ToList()
            };
            return View(model);
        }

        // GET: Manufacturer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
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

        // GET: Manufacturer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manufacturer/Edit/5
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

        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                var manufacturers_id = collection["manufacturers"];
                db.Database.ExecuteSqlCommand($"update shop.oc_manufacturer set status=0 where manufacturer_id in ({manufacturers_id})");
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
