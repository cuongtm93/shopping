using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Customer_IndexViewmodel
    {
        public int customer_id { get; set; }
        public int customer_group_id { get; set; }
        public string customer_group { get; set; }
        public int store_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string ip { get; set; }
        public short status { get; set; }
         public System.DateTime date_added { get; set; }
    }
}