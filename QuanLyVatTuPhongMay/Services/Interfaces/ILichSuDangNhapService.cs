using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface ILichSuDangNhapService
    {
        Task<int> ThemLichSuDangNhapAsync(int maNguoiDung, DateTime thoiDiemDN);
        Task<bool> CapNhatDangXuatAsync(int maLichSu, DateTime thoiDiemDX);
        Task<IEnumerable<LichSuDangNhap>> GetByNguoiDungAsync(int maNguoiDung, DateTime? tuNgay = null, DateTime? denNgay = null);
        Task<IEnumerable<LichSuDangNhap>> GetTatCaLichSuAsync();
        Task<object> GetThongKeSuDungAsync(DateTime tuNgay, DateTime denNgay);
    }
} 