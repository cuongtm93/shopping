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
    
    public partial class oc_customer_history
    {
        public int customer_history_id { get; set; }
        public int customer_id { get; set; }
        public string comment { get; set; }
        public System.DateTime date_added { get; set; }
    }
}
