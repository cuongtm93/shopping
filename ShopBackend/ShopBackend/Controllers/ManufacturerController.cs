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
            var model = new Manufacturer_CreateViewmodel()
            {
                image = "no_image.png",
                sort_order = 0,
                name = ""
            };
            return View(model);
        }

        // POST: Manufacturer/Create
        [HttpPost]
        public ActionResult Create(Manufacturer_CreateViewmodel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.name))
                    {
                        model.name = "";
                    }
                    if (string.IsNullOrEmpty(model.image))
                    {
                        model.image = "no_image.png";
                    }
                    db.oc_manufacturer.Add(new oc_manufacturer()
                    {
                        image = model.image,
                        name  = model.name,
                        sort_order = model.sort_order,
                        status = 1,
                    });
                    db.SaveChanges();
                }
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
            var manufacturer = db.oc_manufacturer.Find(id);
            var model = new Manufacturer_EditViewmodel()
            {
                image = manufacturer.image,
                name =manufacturer.name,
                sort_order = manufacturer.sort_order,
                manufacturer_id = id
            };
            return View(model);
        }

        // POST: Manufacturer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Manufacturer_EditViewmodel model)
        {
            try
            {
                var manufacturer = db.oc_manufacturer.Find(id);
                manufacturer.image = model.image;
                manufacturer.name = model.name;
                manufacturer.sort_order = model.sort_order;
                manufacturer.status = 1;
                db.SaveChanges();
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
