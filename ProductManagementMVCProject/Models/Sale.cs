using System;
using System.Collections.Generic;

namespace ProductManagementMVCProject.Models
{
    public partial class Sale
    {
        public string? ProductName { get; set; }
        public int? SalesYear { get; set; }
        public decimal? SalesAmount { get; set; }
    }
}
