using System.ComponentModel.DataAnnotations;

namespace NgoManhHung_Tuan345.Models
{
    public class ApiRegistrationModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải dài tối thiểu 6 ký tự")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        public string FullName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Age { get; set; }

        [StringLength(5, ErrorMessage = "Tên viết tắt tối đa 5 ký tự")]
        public string? Initials { get; set; }

        public string? Role { get; set; }
    }
}
