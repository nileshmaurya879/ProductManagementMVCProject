﻿using ProductManagementMVCProject.Models;

namespace ProductManagementMVCProject.Dto
{
    public class tblProductDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductDescription { get; set; }

        public virtual TblCategory? Category { get; set; }
    }
}
