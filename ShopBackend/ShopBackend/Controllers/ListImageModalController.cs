using ShopBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gobln.Pager;
using System.IO;

namespace ShopBackend.Controllers
{
    [Authorize]
    public class ListImageModalController : Controller
    {
        const int PAGE_SIZE = 12;
        // GET: ListImageModal
        public ActionResult Index(string dir, int? page = 1)
        {
            var files = ImageGalleryHelper.BrowseFiles(dir);

            var folders = ImageGalleryHelper.BrowseFolders(dir);


            var items = new Dictionary<string, string>();

            folders.ToList().ForEach(folder =>
            {
                items.Add(folder, "folder");
            });

            files.ToList().ForEach(file =>
            {
                items.Add(file, "file");
            });

            var counter = 0;
            var paged_items = new Dictionary<string, string>();
            for (int i = 0; i < items.Count; i++)
            {
                var number_of_skip_items = PAGE_SIZE * (page - 1);
                if (i >= number_of_skip_items && counter < PAGE_SIZE)
                {
                    paged_items.Add(items.Keys.ElementAt(i), items.Values.ElementAt(i));
                    counter++;
                }
            }
            ViewBag.Items = paged_items;
            return View();
        }


        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file_upload, string dir_upload)
        {
            if (file_upload != null && file_upload.ContentLength > 0)
                try
                {
                    string path = Path.Combine(ImageGalleryHelper.ROOT_DIR + "\\" + dir_upload,
                    Path.GetFileName(file_upload.FileName));
                    file_upload.SaveAs(path);
                    return Json(new { m = "File uploaded successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { m = "ERROR:" + ex.Message.ToString() });
                }
            else
            {
                return Json(new { m = "ERROR:" + "You have not specified a file." });
            }
        }

        [HttpPost]
        public JsonResult DeleteFiles(string[] selected_relative_paths)
        {
            foreach (var item in selected_relative_paths)
            {
                var absolute_path = ImageGalleryHelper.ROOT_DIR + item;
                if (Directory.Exists(absolute_path)) Directory.Delete(absolute_path);
                if (System.IO.File.Exists(absolute_path)) System.IO.File.Delete(absolute_path);
            }
            return Json(new { m = "ok" });
        }
    }
}