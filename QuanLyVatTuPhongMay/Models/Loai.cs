using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class Loai
    {
        [Key]
        [Display(Name = "Mã loại")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Tên loại không được để trống")]
        [StringLength(100, ErrorMessage = "Tên loại không quá 100 ký tự")]
        [Display(Name = "Tên loại")]
        public string TenLoai { get; set; }

        // Navigation properties
        [Display(Name = "Thiết bị")]
        public virtual ICollection<TrangTB> TrangTBs { get; set; } = new List<TrangTB>();
    }
}