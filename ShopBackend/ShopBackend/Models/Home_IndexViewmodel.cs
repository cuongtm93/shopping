using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public struct Lastest_Orders
    {
        public string Order_id { get; set; }
        public string Customer_name { get; set; }
        public string Status { get; set; }
        public DateTime Date_Added { get; set; }
        public decimal Total { get; set; }


    }
    public class Home_IndexViewmodel
    {
        public int Total_Orders { get; set; }
        public int Total_Sales { get; set; }
        public int Total_Customers { get; set; }
        public int Total_OnlineUsers { get; set; }
        public List<Lastest_Orders> Lastest_Orders { get; set; }
    }
}