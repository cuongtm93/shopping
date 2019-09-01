using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBackend.Models
{
    public class Components_PaginationViewmodel
    {
        /// <summary>
        /// Ví dụ Review/Index?page=
        /// </summary>
        public string href { get; set; }

        public int total_page_count { get; set; }

        public int current_page_index { get; set; }

        public int number_of_displaying_pages { get; set; }
    }
}