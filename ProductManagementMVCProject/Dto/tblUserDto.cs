using System.ComponentModel.DataAnnotations;

namespace ProductManagementMVCProject.Dto
{
    public class tblUserDto
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "First Name is Required!")]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? SendEmailConfirmed { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? UserPassword { get; set; }
        [Required]
        [Compare("UserPassword",ErrorMessage = "Do not match with UserPassword")]
        public string? ConfirmPassword { get; set; }
    }
}
