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
    
    public partial class oc_currency
    {
        public int currency_id { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string symbol_left { get; set; }
        public string symbol_right { get; set; }
        public string decimal_place { get; set; }
        public decimal value { get; set; }
        public short status { get; set; }
        public System.DateTime date_modified { get; set; }
    }
}