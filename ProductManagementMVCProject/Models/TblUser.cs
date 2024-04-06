using System;
using System.Collections.Generic;

namespace ProductManagementMVCProject.Models
{
    public partial class TblUser
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? SendEmailConfirmed { get; set; }
        public string? UserPassword { get; set; }
    }
}
