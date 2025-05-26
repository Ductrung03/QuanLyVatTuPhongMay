using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVatTuPhongMay.Models
{
    public class LichTruc
    {
        [Key]
        [Display(Name = "Mã lịch")]
        public int MaLich { get; set; }

        [Display(Name = "Ngày")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Ngay { get; set; }

        [Required(ErrorMessage = "Nhân viên không được để trống")]
        [Display(Name = "Mã nhân viên")]
        public int MaNV { get; set; }

        [Required(ErrorMessage = "Phòng máy không được để trống")]
        [Display(Name = "Mã phòng máy")]
        public int MaPhongMay { get; set; }

        // Navigation properties
        [ForeignKey("MaNV")]
        [Display(Name = "Nhân viên")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaPhongMay")]
        [Display(Name = "Phòng máy")]
        public virtual PhongMay PhongMay { get; set; }
    }
}
