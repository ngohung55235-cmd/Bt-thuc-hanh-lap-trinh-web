using System.ComponentModel.DataAnnotations;

namespace NgoManhHung_Tuan345.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [Range(0.01, 99999999.99, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public List<ProductImage>? Images { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
