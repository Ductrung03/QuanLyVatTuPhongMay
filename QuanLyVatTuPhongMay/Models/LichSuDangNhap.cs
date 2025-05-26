using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class LichSuDangNhap
    {
        [Key]
        public int MaLichSu { get; set; }

        public int? MaNguoiDung { get; set; }

        public DateTime? ThoiDiemDN { get; set; }

        public DateTime? ThoiDiemDX { get; set; }

        // Navigation properties
        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ICollection<NhatKyThayDoi> NhatKyThayDois { get; set; }
    }

}
