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
    
    public partial class oc_category
    {
        public int category_id { get; set; }
        public string image { get; set; }
        public int parent_id { get; set; }
        public short top { get; set; }
        public int column { get; set; }
        public int sort_order { get; set; }
        public short status { get; set; }
        public System.DateTime date_added { get; set; }
        public System.DateTime date_modified { get; set; }
    }
}