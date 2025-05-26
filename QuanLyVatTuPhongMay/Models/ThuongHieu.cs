using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class ThuongHieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaThuongHieu { get; set; }

        [Required]
        [StringLength(50)]
        public string TenThuongHieu { get; set; }

        // Navigation properties
        public virtual ICollection<TrangTB> TrangTBs { get; set; }
    }
}
