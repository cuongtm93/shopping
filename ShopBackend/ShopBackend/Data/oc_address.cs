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
    
    public partial class oc_address
    {
        public int address_id { get; set; }
        public int customer_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string company { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public int country_id { get; set; }
        public int zone_id { get; set; }
        public string custom_field { get; set; }
    }
}
