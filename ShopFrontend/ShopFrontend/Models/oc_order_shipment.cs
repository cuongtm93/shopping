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
    
    public partial class oc_order_shipment
    {
        public int order_shipment_id { get; set; }
        public int order_id { get; set; }
        public System.DateTime date_added { get; set; }
        public string shipping_courier_id { get; set; }
        public string tracking_number { get; set; }
    }
}
