using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class NhatKyThayDoi
    {
        [Key]
        public int MaNhatKy { get; set; }

        public int? MaLichSu { get; set; }

        public string? NoiDung { get; set; }

        public string? ThongTinCu { get; set; }

        public string? ThongTinMoi { get; set; }

        // Navigation properties
        [ForeignKey("MaLichSu")]
        public virtual LichSuDangNhap? LichSuDangNhap { get; set; }
    }
}
