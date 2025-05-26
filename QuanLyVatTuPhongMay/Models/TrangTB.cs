using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class TrangTB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTTB { get; set; }

        public int? MaPhongMay { get; set; }

        public int? MaLoai { get; set; }

        public int? GiaTien { get; set; }

        [StringLength(100)]
        public string? TinhTrang { get; set; }

        [Required]
        public DateTime NgayNhap { get; set; }

        public int? SoLanSua { get; set; }

        public int? MaThuongHieu { get; set; }

        public int? MaLich { get; set; }

        // Navigation properties
        [ForeignKey("MaPhongMay")]
        public virtual PhongMay? PhongMay { get; set; }

        [ForeignKey("MaLoai")]
        public virtual Loai? Loai { get; set; }

        [ForeignKey("MaThuongHieu")]
        public virtual ThuongHieu? ThuongHieu { get; set; }

        [ForeignKey("MaLich")]
        public virtual LichThucHanh? LichThucHanh { get; set; }
    }
}
