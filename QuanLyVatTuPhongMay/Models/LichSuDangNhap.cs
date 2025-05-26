using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVatTuPhongMay.Models
{
    public class LichSuDangNhap
    {
        [Key]
        [Display(Name = "Mã lịch sử")]
        public int MaLichSu { get; set; }

        [Display(Name = "Mã người dùng")]
        public int? MaNguoiDung { get; set; }

        [Display(Name = "Thời điểm đăng nhập")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? ThoiDiemDN { get; set; }

        [Display(Name = "Thời điểm đăng xuất")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? ThoiDiemDX { get; set; }

        // Navigation properties
        [ForeignKey("MaNguoiDung")]
        [Display(Name = "Người dùng")]
        public virtual NguoiDung? NguoiDung { get; set; }

        [Display(Name = "Nhật ký thay đổi")]
        public virtual ICollection<NhatKyThayDoi> NhatKyThayDois { get; set; } = new List<NhatKyThayDoi>();

        // Computed properties
        [Display(Name = "Thời gian sử dụng (phút)")]
        [NotMapped]
        public int? ThoiGianSuDung => ThoiDiemDX.HasValue && ThoiDiemDN.HasValue ?
            (int?)(ThoiDiemDX.Value - ThoiDiemDN.Value).TotalMinutes : null;
    }

}
