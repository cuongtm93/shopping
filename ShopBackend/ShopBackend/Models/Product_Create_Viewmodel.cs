using ShopBackend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Product_Create_Viewmodel
    {
        public oc_product_description Oc_product_description { get; set; }
        public oc_product Oc_Product { get; set; }
        public List<oc_tax_class> Tax_Classes { get; set; }
        public List<oc_stock_status> Stock_Statuses { get; set; }
        public List<oc_weight_class_description> Weight_Classes { get; set; }
        public List<oc_length_class_description> Length_Classes { get; set; }
        public Edit_ImagePartiallViewmodel Images { get; internal set; }
    }
}