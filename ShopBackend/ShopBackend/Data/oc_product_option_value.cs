//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopBackend.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class oc_product_option_value
    {
        public int product_option_value_id { get; set; }
        public int product_option_id { get; set; }
        public int product_id { get; set; }
        public int option_id { get; set; }
        public int option_value_id { get; set; }
        public int quantity { get; set; }
        public short subtract { get; set; }
        public decimal price { get; set; }
        public string price_prefix { get; set; }
        public int points { get; set; }
        public string points_prefix { get; set; }
        public decimal weight { get; set; }
        public string weight_prefix { get; set; }
    }
}
