using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class NguoiDung
    {
        [Key]
        [Display(Name = "Mã người dùng")]
        public int MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không quá 50 ký tự")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6-50 ký tự")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        // Navigation properties
        [Display(Name = "Lịch sử đăng nhập")]
        public virtual ICollection<LichSuDangNhap> LichSuDangNhaps { get; set; } = new List<LichSuDangNhap>();

        [Display(Name = "Lịch thực hành")]
        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; } = new List<LichThucHanh>();
    }
}