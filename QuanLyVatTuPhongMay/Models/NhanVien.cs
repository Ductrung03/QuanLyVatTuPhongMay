using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [StringLength(100)]
        public string? HoTen { get; set; }

        // Navigation properties
        public virtual ICollection<LichTruc> LichTrucs { get; set; }
    }

}
