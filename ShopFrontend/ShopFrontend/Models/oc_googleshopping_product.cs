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
    
    public partial class oc_googleshopping_product
    {
        public long product_advertise_google_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public int store_id { get; set; }
        public Nullable<short> has_issues { get; set; }
        public string destination_status { get; set; }
        public int impressions { get; set; }
        public int clicks { get; set; }
        public int conversions { get; set; }
        public decimal cost { get; set; }
        public decimal conversion_value { get; set; }
        public string google_product_category { get; set; }
        public string condition { get; set; }
        public Nullable<short> adult { get; set; }
        public Nullable<int> multipack { get; set; }
        public Nullable<short> is_bundle { get; set; }
        public string age_group { get; set; }
        public Nullable<int> color { get; set; }
        public string gender { get; set; }
        public string size_type { get; set; }
        public string size_system { get; set; }
        public Nullable<int> size { get; set; }
        public short is_modified { get; set; }
    }
}
