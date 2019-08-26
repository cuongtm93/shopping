using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopBackend.Data;
namespace ShopBackend.Models
{
    public class Edit_ImagePartiallViewmodel
    {
        public int Product_id { get; set; }
        public string Image { get; set; }
        public oc_product_image[] Other_images { get; set; }

    }
}