using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Order_PrintInvoiceViewmodel
    {

        public int order_id { get; set; }
        public string invoice_prefix { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_website { get; set; }
        public string store_email { get; set; }
        public string store_phone { get; set; }

        public DateTime date_added { get; set; }
        public string payment_method { get; set; }
        public string shipping_method { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string shipping_city { get; set; }
        public string shipping_country { get; set; }
        public string shipping_address1 { get; set; }
        public string shipping_address2 { get; set; }
        public string payment_city { get; set; }
        public string payment_country { get; set; }
        public string payment_address1 { get; set; }
        public string payment_address2 { get; set; }

        /// <summary>
        ///  Danh sách sản phẩm đã mua
        /// </summary>
        public List<Data.oc_order_product> order_products { get; set; }

        /// <summary>
        ///  Tổng tiền phải trả
        /// </summary>
        public decimal total { get; set; }

        public int store_id { get; internal set; }

        /// <summary>
        ///  Tổng tiền hàng
        /// </summary>
        public decimal order_products_total;


    }
}