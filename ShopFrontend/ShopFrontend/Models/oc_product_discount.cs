//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopFrontend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class oc_product_discount
    {
        public int product_discount_id { get; set; }
        public int product_id { get; set; }
        public int customer_group_id { get; set; }
        public int quantity { get; set; }
        public int priority { get; set; }
        public decimal price { get; set; }
        public Nullable<System.DateTime> date_start { get; set; }
        public System.DateTime date_end { get; set; }
    }
}