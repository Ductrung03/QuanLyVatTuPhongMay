using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVatTuPhongMay.Models
{
    public class ThuongHieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã thương hiệu")]
        public int MaThuongHieu { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [StringLength(50, ErrorMessage = "Tên thương hiệu không quá 50 ký tự")]
        [Display(Name = "Tên thương hiệu")]
        public string TenThuongHieu { get; set; }

        // Navigation properties
        [Display(Name = "Thiết bị")]
        public virtual ICollection<TrangTB> TrangTBs { get; set; } = new List<TrangTB>();
    }

}