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
    
    public partial class oc_customer_transaction
    {
        public int customer_transaction_id { get; set; }
        public int customer_id { get; set; }
        public int order_id { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public System.DateTime date_added { get; set; }
    }
}
