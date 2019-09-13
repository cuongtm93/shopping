using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopFrontend.Models
{
    public class Home_CategoryViewmodel
    {
        public List<Product> products { get; set; }

        public IEnumerable<oc_category_description> toplevel_categories { get; set; }
    }
}