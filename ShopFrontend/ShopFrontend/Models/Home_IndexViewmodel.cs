using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopFrontend.Models
{
    public class Product
    {
        public int product_id { get; set; }
        public string model { get; set; }
        public IEnumerable<oc_product_image> alternative_images;
        public string name { get; set; }
        public string image { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public int points { get; set; }
        public System.DateTime date_available { get; set; }
        public decimal weight { get; set; }
        public int weight_class_id { get; set; }
        public decimal length { get; set; }
        public decimal width { get; set; }
        public decimal height { get; set; }
        public int length_class_id { get; set; }
        public short subtract { get; set; }
        public int minimum { get; set; }
        public int sort_order { get; set; }
        public short status { get; set; }
        public int viewed { get; set; }
        public System.DateTime date_added { get; set; }
        public System.DateTime date_modified { get; set; }
        public Nullable<int> deleted { get; set; }
    }
    public class Home_IndexViewmodel
    {
        public IEnumerable<Product> top_4_news_products { get; set; }

        public IEnumerable<Product> top_4_maybeyoulike_products { get; set; }
    }
}