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
    
    public partial class oc_product_option
    {
        public int product_option_id { get; set; }
        public int product_id { get; set; }
        public int option_id { get; set; }
        public string value { get; set; }
        public short required { get; set; }
    }
}
