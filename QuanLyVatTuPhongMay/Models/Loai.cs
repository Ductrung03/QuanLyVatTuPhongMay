using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLoai { get; set; }

        // Navigation properties
        public virtual ICollection<TrangTB> TrangTBs { get; set; }
    }
}
