using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Product_Index_Viewmodel
    {
        public int product_id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
    }
}