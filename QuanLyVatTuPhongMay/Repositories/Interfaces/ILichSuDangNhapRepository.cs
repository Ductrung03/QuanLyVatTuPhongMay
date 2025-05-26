using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface ILichSuDangNhapRepository : IBaseRepository<LichSuDangNhap>
    {
        Task<int> ThemLichSuDangNhapAsync(int maNguoiDung, DateTime thoiDiemDN);
        Task<bool> CapNhatDangXuatAsync(int maLichSu, DateTime thoiDiemDX);
        Task<IEnumerable<LichSuDangNhap>> GetByNguoiDungAsync(int maNguoiDung,
            DateTime? tuNgay = null, DateTime? denNgay = null);
    }

}