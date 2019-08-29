using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public struct Manufacturer
    {
        public int Manufacturer_id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int Sort_order { get; set; }
    }

    public class Manufacturer_IndexViewmodel
    {
        public  List<Manufacturer> Manufacturers { get; set; }
    }
}