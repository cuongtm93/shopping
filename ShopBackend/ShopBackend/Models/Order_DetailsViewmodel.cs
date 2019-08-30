using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Order_History
    {
        public DateTime date_added { get; set; }
        public string comment { get; set; }
        public string status { get; set; }
        public bool customer_notified { get; set; }
    }

    public class Order_DetailsViewmodel
    {
        
        
        public int order_id { get; set; }
        public string store_name { get; set; }
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

        /// <summary>
        ///  Tổng tiền hàng
        /// </summary>
        public decimal order_products_total;

        /// <summary>
        /// danh sách lịch sử đơn hàng
        /// </summary>
        public List<Order_History> order_history_records { get; set; }

        /// <summary>
        /// Danh sách các trạng thái của đơn hàng
        /// </summary>
        public List<Data.oc_order_status> order_statuses { get; set; }

    }
}