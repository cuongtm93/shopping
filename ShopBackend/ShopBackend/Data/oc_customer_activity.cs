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
    
    public partial class oc_customer_activity
    {
        public int customer_activity_id { get; set; }
        public int customer_id { get; set; }
        public string key { get; set; }
        public string data { get; set; }
        public string ip { get; set; }
        public System.DateTime date_added { get; set; }
    }
}
