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
    
    public partial class oc_order_option
    {
        public int order_option_id { get; set; }
        public int order_id { get; set; }
        public int order_product_id { get; set; }
        public int product_option_id { get; set; }
        public int product_option_value_id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }
}
