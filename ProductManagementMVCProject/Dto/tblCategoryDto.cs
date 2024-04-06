using System.ComponentModel.DataAnnotations;

namespace ProductManagementMVCProject.Dto
{
    public class tblCategoryDto
    {
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? CategoryDescription { get; set; }
    }
}
