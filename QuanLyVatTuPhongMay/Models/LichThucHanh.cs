using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyVatTuPhongMay.Models
{
    public class LichThucHanh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã lịch")]
        public int MaLich { get; set; }

        [StringLength(50, ErrorMessage = "Trạng thái không quá 50 ký tự")]
        [Display(Name = "Trạng thái")]
        public string? TrangThai { get; set; } = "Đã đặt";

        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống")]
        [Display(Name = "Thời gian bắt đầu")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime ThoiGianBD { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống")]
        [Display(Name = "Thời gian kết thúc")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime ThoiGianKT { get; set; }

        [Display(Name = "Mã người dùng")]
        public int? MaNguoiDung { get; set; }

        // Navigation properties
        [ForeignKey("MaNguoiDung")]
        [Display(Name = "Người đặt")]
        public virtual NguoiDung? NguoiDung { get; set; }

        [Display(Name = "Thiết bị sử dụng")]
        public virtual ICollection<TrangTB> TrangTBs { get; set; } = new List<TrangTB>();

        // Computed properties
        [Display(Name = "Thời gian sử dụng (phút)")]
        [NotMapped]
        public int ThoiGianSuDung => (int)(ThoiGianKT - ThoiGianBD).TotalMinutes;

        [Display(Name = "Trạng thái chi tiết")]
        [NotMapped]
        public string TrangThaiChiTiet
        {
            get
            {
                if (TrangThai == "Đã hủy") return "Đã hủy";
                if (DateTime.Now < ThoiGianBD) return "Chưa bắt đầu";
                if (DateTime.Now >= ThoiGianBD && DateTime.Now <= ThoiGianKT) return "Đang thực hiện";
                if (DateTime.Now > ThoiGianKT) return "Đã kết thúc";
                return TrangThai ?? "Không xác định";
            }
        }
    }
}