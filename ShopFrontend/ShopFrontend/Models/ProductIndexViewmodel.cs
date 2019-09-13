using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopFrontend.Models
{
    public class ProductIndexViewmodel
    {
        public oc_product product { get; set; }
        public oc_product_description product_description { get; set; }
        public IOrderedQueryable<oc_product_image> product_alternative_images { get; internal set; }
        public List<oc_category_description> breadscrumb { get; set; }
    }
}