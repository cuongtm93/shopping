using ShopFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopFrontend.Controllers
{
    public class CustomerController : Controller
    {
        private shop2Entities db = new shop2Entities();
        // GET: Customer
        public ActionResult Register()
        {
            return View();
        }

        [Authorize]
        public new ActionResult Profile(string email)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            var hash_bytes = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            var password_hash = string.Concat(hash_bytes.Select(b => b.ToString("x2")));
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                try
                {
                    MailAddress m = new MailAddress(username);
                }
                catch (Exception)
                {
                    ViewBag.Error = "Email không hợp lệ";
                    return View();
                }
                ViewBag.Error = "email hoặc mật khẩu không hợp lệ";
                return View();
            }
            if (db.oc_customer.Any(r => r.email == username))
            {
                ViewBag.Error = "Email đã tồn tại";
                return View();
            }

            db.oc_customer.Add(new oc_customer()
            {
                email = username,
                password = password_hash,
                customer_group_id = 1, // mặc định,
                firstname = "tên",
                lastname = "họ",
                status = 1,
                address_id = 0, // không có;
                date_added = DateTime.Now,
                custom_field = "",
                cart = "",
                code = "",
                fax = "",
                ip = "",
                token = "",
                safe = 1,
                newsletter = 0,
                salt = "",
                language_id = 1,
                store_id = 1,
                telephone = "sđt",
                wishlist = "",
            });
            db.SaveChanges();
            FormsAuthentication.SetAuthCookie(username, true);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            var hash_bytes = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            var password_hash = string.Concat(hash_bytes.Select(b => b.ToString("x2")));
            var authenicated = db.oc_customer.Any(r => r.email == username && r.password == password_hash);

            if (!authenicated)
            {
                ViewBag.Error = "Kiểm tra lại email và mật khẩu";
                return View();
            }
            var customer = db.oc_customer.Where(r => r.email == username).SingleOrDefault();
            customer.ip = Request.UserHostAddress;
            db.oc_customer_login.Add(new oc_customer_login()
            {
                date_added = DateTime.Now,
                date_modified = DateTime.Now,
                email = username,
                ip = customer.ip,
                total = 0
            });
            db.SaveChanges();
            FormsAuthentication.SetAuthCookie(username, true);
            //So that the user can be referred back to where they were when they click logon
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = "/";

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}