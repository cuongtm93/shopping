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
    
    public partial class oc_translation
    {
        public int translation_id { get; set; }
        public int store_id { get; set; }
        public int language_id { get; set; }
        public string route { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public System.DateTime date_added { get; set; }
    }
}
