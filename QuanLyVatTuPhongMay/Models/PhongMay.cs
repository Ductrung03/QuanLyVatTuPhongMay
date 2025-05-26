using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class PhongMay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã phòng máy")]
        public int MaPhongMay { get; set; }

        [Required(ErrorMessage = "Tên phòng máy không được để trống")]
        [StringLength(50, ErrorMessage = "Tên phòng máy không quá 50 ký tự")]
        [Display(Name = "Tên phòng máy")]
        public string TenPhongMay { get; set; }

        // Navigation properties
        [Display(Name = "Lịch trực")]
        public virtual ICollection<LichTruc> LichTrucs { get; set; } = new List<LichTruc>();

        [Display(Name = "Thiết bị")]
        public virtual ICollection<TrangTB> TrangTBs { get; set; } = new List<TrangTB>();
    }
}