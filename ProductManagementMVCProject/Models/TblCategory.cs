using System;
using System.Collections.Generic;

namespace ProductManagementMVCProject.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
