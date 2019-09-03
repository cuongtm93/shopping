using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBackend.Data;
using System.Security.Cryptography;
using System.Text;
using ShopBackend.Models;

namespace ShopBackend.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly shop2Entities db;
        ~HomeController()
        {
            db.Dispose();
        }
        public HomeController()
        {
            db = new shop2Entities();
        }

        public ActionResult Index()
        {
            var Lastest_Orders = db.oc_order
                .OrderByDescending(r => r.date_added)
                .Take(5).ToList();
            const int COMPLETE = 5;
            var model = new Home_IndexViewmodel()
            {
                Lastest_Orders = new List<Lastest_Orders>(),
                Total_Orders = db.oc_order.Count(),
                Total_Customers = db.oc_customer.Count(),
                Total_Sales = db.oc_order.Count(r => r.order_status_id == COMPLETE),
                Total_OnlineUsers = db.oc_customer_online.Count(),
            };

            foreach (var order in Lastest_Orders)
            {
                var customer = db.oc_customer.Single(r => r.customer_id == order.customer_id);
                var status = db.oc_order_status.Single(r => r.order_status_id == order.order_status_id);
                model.Lastest_Orders.Add(new Models.Lastest_Orders()
                {
                    Customer_name = customer.firstname + customer.lastname,
                    Status = status.name,
                    Date_Added = order.date_added,
                    Order_id = order.order_id.ToString(),
                    Total = (int)order.total
                });
            }
            return View(model);
        }
    }
}