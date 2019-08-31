using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Order_PrintShipping_Products
    {
        public string location { get; set; }
        public string name { get; set; }
        public decimal weight { get; set; }
        public string weight_unit { get; set; }
        public string model { get; set; }
        public decimal quantity { get; set; }
    }
    public class Order_PrintShippingViewmodel
    {
        public int order_id { get; set; }
        public string invoice_prefix { get; set; }

        public int store_id { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_website { get; set; }
        public string store_email { get; set; }
        public string store_phone { get; set; }

        public DateTime date_added { get; set; }
        public string payment_method { get; set; }
        public string shipping_method { get; set; }

        public string customer_full_name { get; set; }
        public string customer_email { get; set; }
        public string customer_telephone { get; set; }

        public string shipping_city { get; set; }
        public string shipping_country { get; set; }
        public string shipping_address1 { get; set; }
        public string shipping_address2 { get; set; }
        public string payment_city { get; set; }
        public string payment_country { get; set; }
        public string payment_address1 { get; set; }
        public string payment_address2 { get; set; }

        public List<Order_PrintShipping_Products> Products { get; set; }
    }
}