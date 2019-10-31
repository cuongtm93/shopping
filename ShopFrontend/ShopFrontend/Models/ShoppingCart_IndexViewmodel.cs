using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopFrontend.Models
{
    public class ShoppingCart_Index
    {
        public int product_id { get; set; }
        public int quantity { get; set; }
        public string product_name { get; set; }

        public decimal product_price { get; set; }
    }
    public class ShoppingCart_IndexViewmodel
    {
        public List<ShoppingCart_Index> List { get; set; }

        public oc_address customer_address { get; set; }
        public decimal total { get; set; }
    }
}