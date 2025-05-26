using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyVatTuPhongMay.Models
{
    public class TrangTB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã thiết bị")]
        public int MaTTB { get; set; }

        [Display(Name = "Mã phòng máy")]
        public int? MaPhongMay { get; set; }

        [Display(Name = "Mã loại")]
        public int? MaLoai { get; set; }

        [Display(Name = "Giá tiền")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0")]
        public int? GiaTien { get; set; }

        [StringLength(100, ErrorMessage = "Tình trạng không quá 100 ký tự")]
        [Display(Name = "Tình trạng")]
        public string? TinhTrang { get; set; }

        [Required(ErrorMessage = "Ngày nhập không được để trống")]
        [Display(Name = "Ngày nhập")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhap { get; set; }

        [Display(Name = "Số lần sửa")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lần sửa phải >= 0")]
        public int? SoLanSua { get; set; } = 0;

        [Display(Name = "Mã thương hiệu")]
        public int? MaThuongHieu { get; set; }

        [Display(Name = "Mã lịch")]
        public int? MaLich { get; set; }

        // Navigation properties
        [ForeignKey("MaPhongMay")]
        [Display(Name = "Phòng máy")]
        public virtual PhongMay? PhongMay { get; set; }

        [ForeignKey("MaLoai")]
        [Display(Name = "Loại thiết bị")]
        public virtual Loai? Loai { get; set; }

        [ForeignKey("MaThuongHieu")]
        [Display(Name = "Thương hiệu")]
        public virtual ThuongHieu? ThuongHieu { get; set; }

        [ForeignKey("MaLich")]
        [Display(Name = "Lịch thực hành")]
        public virtual LichThucHanh? LichThucHanh { get; set; }

        // Computed properties
        [Display(Name = "Số ngày sử dụng")]
        [NotMapped]
        public int SoNgayDaSuDung => (DateTime.Now - NgayNhap).Days;

        [Display(Name = "Trạng thái hoạt động")]
        [NotMapped]
        public string TrangThaiHoatDong => SoLanSua >= 5 ? "Cần bảo trì" :
                                          TinhTrang?.Contains("hỏng") == true ? "Hỏng" : "Bình thường";
    }
}