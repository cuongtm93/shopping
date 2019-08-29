using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Manufacturer_CreateViewmodel
    {
        [Required]
        public string name { get; set; }

        public string image { get; set; }

        public int sort_order { get; set; }
    }
}