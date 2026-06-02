using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NgoManhHung_Tuan345.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; }

        public string? Address { get; set; }

        public string? Age { get; set; }
    }
}
