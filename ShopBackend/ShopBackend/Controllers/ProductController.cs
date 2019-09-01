using System.Linq;
using System.Web.Mvc;
using ShopBackend.Data;
using Gobln.Pager;
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
        ~ProductController()
        {
            db.Dispose();
        }
        public ProductController()
        {
            db = new shop2Entities();
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        [HttpPost]
        public JsonResult add_more_minor_image(int product_id)
        {
            var created = db.oc_product_image.Add(new oc_product_image()
            {
                image = "no_image.png",
                product_id = product_id,
                sort_order = 0,
            });
            db.SaveChanges();
            return Json(new { m = "ok", created = created.product_image_id });
        }

        [HttpPost]
        public JsonResult Change_Main_Product_Image(int product_id, string new_image_path)
        {
            var product = db.oc_product.Find(product_id);
            product.image = new_image_path;
            db.SaveChanges();
            return Json(new { m = "ok" });
        }
        [HttpPost]
        public JsonResult Reset_Main_Product_Image(int product_id)
        {
            const string DEFAULT_PRODUCT_IMAGE_PATH = "no_image.png";
            var product = db.oc_product.Find(product_id);
            product.image = DEFAULT_PRODUCT_IMAGE_PATH;
            db.SaveChanges();
            return Json(new { m = "ok" });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="product_minor_image_id"> Có dạng _id , ví dụ _28 </param>
        /// <param name="new_image_path"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Change_Minor_Product_Image(int product_id, string product_minor_image_id, string new_image_path)
        {
            int real_product_minor_image_id = int.Parse(product_minor_image_id.Substring(1));
            var product_minor_image = db.oc_product_image.Find(real_product_minor_image_id);
            product_minor_image.image = new_image_path;
            db.SaveChanges();
            return Json(new { m = "ok" });
        }
        [HttpPost]
        public JsonResult Remove_Minor_Product_Image(int product_id, string product_minor_image_path)
        {
            var selected_images = db.oc_product_image.Where(r => r.product_id == product_id && r.image == product_minor_image_path);
            db.oc_product_image.RemoveRange(selected_images);
            db.SaveChanges();
            return Json(new { m = "ok" });
        }
        // GET: Product
        public ActionResult Index(int? page)
        {
            const int NOT_DELETED = 1;
            const int FIRST_PAGE = 1;
            var model = from tbl1 in db.oc_product
                        join tbl2 in db.oc_product_description on tbl1.product_id equals tbl2.product_id
                        where tbl1.status == NOT_DELETED
                        orderby tbl1.date_added
                        select new Product_Index_Viewmodel
                        {
                            image = tbl1.image,
                            status = (tbl1.status == 1) ? "dừng " : "hoạt động",
                            model = tbl1.model,
                            name = tbl2.name,
                            product_id = tbl1.product_id,
                            quantity = tbl1.quantity
                        };

            if (!page.HasValue) page = FIRST_PAGE;
            return View(model.ToPagedList(PAGE_SIZE).GetPage(page.Value));
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var model = new oc_product_description()
            {
                language_id = 1
            };
            return View(model);
        }

        // POST: Product/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(oc_product_description model)
        {
            try
            {
                // Thêm Product
                var n_product = new oc_product()
                {
                    date_added = DateTime.Now,
                    image = "no_image.png",
                    date_available = DateTime.Now,
                    model = "",
                    date_modified = DateTime.Now,
                    ean = "",
                    height = default,
                    isbn = "",
                    jan = "",
                    length = default,
                    length_class_id = db.oc_length_class.First().length_class_id,
                    location = "",
                    manufacturer_id = db.oc_manufacturer.First().manufacturer_id,
                    minimum = default,
                    mpn = "",
                    points = default,
                    price = default,
                    width = default,
                    quantity = default,
                    shipping = default,
                    sku = "",
                    sort_order = default,
                    status = 1,
                    subtract = default,
                    stock_status_id = default,
                    tax_class_id = default,
                    upc = "",
                    weight = default,
                    weight_class_id = db.oc_weight_class.First().weight_class_id
                };

                var n_product_id = db.oc_product.Add(n_product).product_id;
                db.SaveChanges();
                var n_product_description = model;
                n_product_description.meta_title = (model.meta_title == null ? "" : model.meta_title);
                n_product_description.meta_keyword = model.meta_keyword == null ? "" : model.meta_keyword;
                n_product_description.name = model.name == null ? "" : model.name;
                n_product_description.description = model.description == null ? "" : model.description;
                n_product_description.meta_description = model.meta_description == null ? "" : model.meta_description;
                n_product_description.product_id = n_product.product_id;
                n_product_description.language_id = db.oc_language.First().language_id;
                n_product_description.tag = "";
                db.oc_product_description.Add(n_product_description);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = n_product.product_id, tab = "tab_image" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id, string tab = "tab_general")
        {
            var product_details = db.oc_product.Find(id);
            var model = new Product_Create_Viewmodel()
            {
                Oc_product_description = db.oc_product_description.Find(id, 1),
                Oc_Product = product_details,
                Tax_Classes = db.oc_tax_class.ToList(),
                Weight_Classes = db.oc_weight_class_description.ToList(),
                Length_Classes = db.oc_length_class_description.ToList(),
                Stock_Statuses = db.oc_stock_status.ToList(),

                Images = new Edit_ImagePartialViewmodel()
                {
                    Product_id = product_details.product_id,
                    Image = product_details.image,
                    Other_images = db.oc_product_image.Where(r => r.product_id == id).ToArray()
                }
            };

            ViewBag.Tax_Classes = model.Tax_Classes;
            ViewBag.Stock_Statuses = model.Stock_Statuses;
            ViewBag.Weight_Classes = model.Weight_Classes;
            ViewBag.Length_Classes = model.Length_Classes;

            const string DEFAULT_TAB = "tab_general";
            var allowed_tabs = new string[] { DEFAULT_TAB, "tab_data", "tab_property", "tab_image", };
            ViewBag.enabled_tab = allowed_tabs.Contains(tab) ? tab : DEFAULT_TAB;
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
                        product.date_modified = DateTime.Now;
                        db.SaveChanges();
                        break;
                    default:
                        break;
                }
                return Redirect(Request.UrlReferrer.ToString());

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
                var result1 = db.Database.ExecuteSqlCommand($"update shop.oc_product set status=0 where product_id in ({_list_checked_product_id})");
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
