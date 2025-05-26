using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class NhanVien
    {
        [Key]
        [Display(Name = "Mã nhân viên")]
        public int MaNV { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không quá 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        // Navigation properties
        [Display(Name = "Lịch trực")]
        public virtual ICollection<LichTruc> LichTrucs { get; set; } = new List<LichTruc>();
    }

}