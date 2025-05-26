using Cuba_Staterkit.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class LichTruc
    {
        [Key]
        public int MaLich { get; set; }

        public DateTime? Ngay { get; set; }

        [Required]
        public int MaNV { get; set; }

        [Required]
        public int MaPhongMay { get; set; }

        // Navigation properties
        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaPhongMay")]
        public virtual PhongMay PhongMay { get; set; }
    }
}
