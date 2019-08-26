using ShopBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace ShopBackend.Controllers
{
    [Authorize]
    public class ListImageModalController : Controller
    {
        const int PAGE_SIZE = 2;
        // GET: ListImageModal
        public ActionResult Index(string dir, int? page = 1)
        {
            var files = ImageGalleryHelper.BrowseFiles(dir);
            files = files.Skip(PAGE_SIZE * (page.Value - 1));
            files = files.Take(PAGE_SIZE);

            var folders = ImageGalleryHelper.BrowseFolders(dir);
            folders = folders.Skip(PAGE_SIZE * (page.Value - 1));
            folders = folders.Take(PAGE_SIZE);

            ViewBag.files = files;
            ViewBag.folders = folders;

            return View();
        }
    }
}