using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        // Navigation properties
        public virtual ICollection<LichSuDangNhap> LichSuDangNhaps { get; set; }
        public virtual ICollection<LichThucHanh> LichThucHanhs { get; set; }
    }
}
