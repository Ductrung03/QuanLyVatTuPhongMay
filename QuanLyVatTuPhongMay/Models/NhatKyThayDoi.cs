using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVatTuPhongMay.Models
{
    public class NhatKyThayDoi
    {
        [Key]
        [Display(Name = "Mã nhật ký")]
        public int MaNhatKy { get; set; }

        [Display(Name = "Mã lịch sử")]
        public int? MaLichSu { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string? NoiDung { get; set; }

        [Display(Name = "Thông tin cũ")]
        [DataType(DataType.MultilineText)]
        public string? ThongTinCu { get; set; }

        [Display(Name = "Thông tin mới")]
        [DataType(DataType.MultilineText)]
        public string? ThongTinMoi { get; set; }

        // Navigation properties
        [ForeignKey("MaLichSu")]
        [Display(Name = "Lịch sử đăng nhập")]
        public virtual LichSuDangNhap? LichSuDangNhap { get; set; }
    }
}