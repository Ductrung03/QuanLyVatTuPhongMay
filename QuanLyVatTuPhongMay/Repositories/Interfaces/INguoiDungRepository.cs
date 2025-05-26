using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface INguoiDungRepository : IBaseRepository<NguoiDung>
    {
        Task<NguoiDung?> DangNhapAsync(string tenDangNhap, string matKhau);
        Task<bool> KiemTraTenDangNhapTonTaiAsync(string tenDangNhap);
    }
}