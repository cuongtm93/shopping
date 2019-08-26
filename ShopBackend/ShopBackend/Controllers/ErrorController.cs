using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBackend.Views.Home
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult _404()
        {
            return View();
        }
    }
}