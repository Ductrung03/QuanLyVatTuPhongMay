using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class LichThucHanh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLich { get; set; }

        [StringLength(50)]
        public string? TrangThai { get; set; }

        [Required]
        public DateTime ThoiGianBD { get; set; }

        [Required]
        public DateTime ThoiGianKT { get; set; }

        public int? MaNguoiDung { get; set; }

        // Navigation properties
        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung? NguoiDung { get; set; }
        public virtual ICollection<TrangTB> TrangTBs { get; set; }
    }
}
