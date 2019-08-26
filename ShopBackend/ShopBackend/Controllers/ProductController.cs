using System.Linq;
using System.Web.Mvc;
using ShopBackend.Data;
using PagedList;
using System.Collections;
using System.Collections.Generic;
using System;
using Ganss.XSS;
using System.Data.SqlClient;
using ShopBackend.Models;
using System.Web.Routing;
using System.Globalization;

namespace ShopBackend.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private shop2Entities db;
        const int PAGE_SIZE = 5;

        public ProductController()
        {
            db = new shop2Entities();
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        // GET: Product
        public ActionResult Index(int? page)
        {

            var raw = from tbl1 in db.oc_product
                      join tbl2 in db.oc_product_description on tbl1.product_id equals tbl2.product_id
                      orderby tbl1.date_added
                      select new
                      {
                          tbl1.product_id,
                          tbl1.image,
                          tbl2.name,
                          tbl1.model,
                          tbl1.quantity,
                          tbl1.status
                      };

            if (!page.HasValue) page = 1;
            var data = raw.OrderBy(r => r.product_id).ToPagedList(page.Value, PAGE_SIZE);
            var model = new List<Models.Product_Index_Viewmodel>();
            foreach (var item in data)
            {
                model.Add(new Models.Product_Index_Viewmodel()
                {
                    product_id = item.product_id,
                    image = item.image,
                    model = item.model,
                    name = item.name,
                    quantity = item.quantity,
                    status = (item.status == 0) ? "Dừng" : "Hoạt động"
                });
            }
            ViewBag.Page = page;
            ViewBag.PageCount = data.PageCount;
            if (page > data.PageCount && data.PageCount > 0)
            {
                var lastPage = data.PageCount;
                return RedirectToAction("Index", new { page = lastPage });
            }
            return View(model);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var model = new Product_Create_Viewmodel()
            {
                Oc_product_description = new oc_product_description(),
                Oc_Product = new oc_product(),
                Length_Classes = db.oc_length_class_description.ToList(),
                Weight_Classes = db.oc_weight_class_description.ToList(),
                Tax_Classes = db.oc_tax_class.ToList(),
                Stock_Statuses = db.oc_stock_status.ToList(),
            };

            ViewBag.Tax_Classes = model.Tax_Classes;
            ViewBag.Length_Classes = model.Length_Classes;
            ViewBag.Weight_Classes = model.Weight_Classes;
            ViewBag.Stock_Statuses = model.Stock_Statuses;
            return View(model);
        }

        // POST: Product/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Thêm Product
                var n_product = new oc_product()
                {
                    date_added = DateTime.Now,

                };

                var n_product_id = db.oc_product.Add(n_product).product_id;

                db.SaveChanges();

                // Thêm product_description 
                var n_oc_product_description = new oc_product_description()
                {

                };
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id, string tab = "tab_general")
        {
            var product = db.oc_product.Find(id);
            var model = new Product_Create_Viewmodel()
            {
                Oc_product_description = db.oc_product_description.Find(id, 1),
                Oc_Product = product,
                Tax_Classes = db.oc_tax_class.ToList(),
                Weight_Classes = db.oc_weight_class_description.ToList(),
                Length_Classes = db.oc_length_class_description.ToList(),
                Stock_Statuses = db.oc_stock_status.ToList(),
                Images = new Edit_ImagePartiallViewmodel()
                {
                    Image = product.image,
                    Other_images = db.oc_product_image.Where(r => r.product_id == id).ToArray()
                }
            };

            ViewBag.Tax_Classes = model.Tax_Classes;
            ViewBag.Stock_Statuses = model.Stock_Statuses;
            ViewBag.Weight_Classes = model.Weight_Classes;
            ViewBag.Length_Classes = model.Length_Classes;

            var enable_tab = new Dictionary<string, string>();
            var tabs = new string[] { "tab_general", "tab_data", "tab_property", "tab_image", };
            foreach (var _tab in tabs)
            {
                var q = Request.QueryString.ToString().ToLower();
                var _value = _tab == tab ? "in active" : "";
                enable_tab.Add(_tab, _value);
            }

            ViewData["enable_tab"] = enable_tab;
            return View(model);
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }
        // POST: Product/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var product_id = int.Parse(collection["product_id"]);
                // TODO: Add update logic here
                switch (collection["case"])
                {
                    case "product_description":

                        var product_desc = db.oc_product_description.SingleOrDefault(r => r.product_id == product_id);
                        HtmlSanitizer sanitizer = new HtmlSanitizer();

                        var n_name = collection["product_name"];
                        var n_description = collection["product_description"];
                        n_description = System.Net.WebUtility.HtmlEncode(n_description);
                        var n_meta_title = collection["product_meta_title"];
                        var n_meta_description = collection["product_meta_description"];
                        var n_meta_keyword = collection["product_meta_keyword"];

                        product_desc.name = n_name;
                        product_desc.meta_title = n_meta_title;
                        product_desc.meta_keyword = n_meta_keyword;
                        product_desc.meta_description = n_meta_description;
                        product_desc.tag = n_meta_keyword;
                        product_desc.description = n_description;
                        db.SaveChanges();
                        break;
                    case "product_data":
                        var product = db.oc_product.Find(product_id);
                        product.date_modified = DateTime.Now;
                        try
                        {
                            product.date_available = DateTime.ParseExact(collection["date_available"], "mm/dd/yyyy", CultureInfo.InvariantCulture);

                        }
                        catch (Exception ex)
                        {
                            product.date_available = DateTime.Now;
                        }
                        product.ean = collection["ean"];
                        product.height = decimal.Parse(collection["height"]);
                        product.isbn = collection["isbn"];
                        product.jan = collection["jan"];
                        product.length = decimal.Parse(collection["length"]);
                        product.length_class_id = int.Parse(collection["length_class_id"]);
                        product.location = collection["location"];
                        product.minimum = int.Parse(collection["minimum"]);
                        product.model = collection["model"];
                        product.mpn = collection["mpn"];
                        product.price = decimal.Parse(collection["price"]);
                        product.quantity = int.Parse(collection["quantity"]);
                        product.shipping = short.Parse(collection["shipping"]);
                        product.sku = collection["sku"];
                        product.sort_order = int.Parse(collection["sort_order"]);
                        product.status = short.Parse(collection["status"]);
                        product.stock_status_id = int.Parse(collection["stock_status_id"]);
                        product.tax_class_id = int.Parse(collection["tax_class_id"]);
                        product.upc = collection["upc"];
                        product.weight = decimal.Parse(collection["weight"]);
                        product.weight_class_id = int.Parse(collection["weight_class_id"]);
                        product.width = decimal.Parse(collection["width"]);
                        product.subtract = short.Parse(collection["subtract"]);
                        db.SaveChanges();
                        break;
                    default:
                        break;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]

        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                string _list_checked_product_id = collection["checked"];
                var result = db.Database.ExecuteSqlCommand($"delete from shop.oc_product where product_id in ({_list_checked_product_id})");
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}
