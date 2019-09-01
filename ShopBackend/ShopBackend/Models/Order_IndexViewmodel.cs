using Gobln.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Order_IndexViewmodel
    {
        public int order_id { get; set; }

        public string customer_fullname { get; set; }

        public string order_status { get; set; }

        public decimal total { get; set; }

        public DateTime date_added { get; set; }

        public DateTime date_modified { get; set; }
    }
}