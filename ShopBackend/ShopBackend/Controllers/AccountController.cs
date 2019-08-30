using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShopBackend.Data;
namespace ShopBackend.Controllers
{
    public class AccountController : Controller
    {
        private readonly shop2Entities db;
        ~AccountController()
        {
            db.Dispose();
        }
        public AccountController()
        {
            db = new shop2Entities();
        }
        [HttpPost]
        public ActionResult login(oc_user user, string ReturnUrl = "/")
        {
            if (ReturnUrl == "") ReturnUrl = "/";

            if (db.oc_user.Any(r => r.username == user.username && r.password == user.password))
            {

                FormsAuthentication.SetAuthCookie(user.username, true);
                return Redirect(ReturnUrl);
            }
            else
                return View("Index");
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {

            var redirectUrl = FormsAuthentication.GetRedirectUrl(User.Identity.Name, true);
            FormsAuthentication.SignOut();
            return Redirect(redirectUrl);
        }

    }
}