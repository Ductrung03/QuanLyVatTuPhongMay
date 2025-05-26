using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class PhongMay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhongMay { get; set; }

        [StringLength(50)]
        public string? TenPhongMay { get; set; }

        // Navigation properties
        public virtual ICollection<LichTruc> LichTrucs { get; set; }
        public virtual ICollection<TrangTB> TrangTBs { get; set; }
    }
}
