using System.ComponentModel.DataAnnotations;

namespace NgoManhHung_Tuan345.Models
{
    public class ApiLoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; } = string.Empty;
    }
}
