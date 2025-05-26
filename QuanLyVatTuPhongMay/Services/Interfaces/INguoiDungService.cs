using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface INguoiDungService : IBaseService<NguoiDung>
    {
        Task<NguoiDung?> DangNhapAsync(string tenDangNhap, string matKhau);
        Task<bool> KiemTraTenDangNhapTonTaiAsync(string tenDangNhap);
        Task<bool> DoiMatKhauAsync(int maNguoiDung, string matKhauCu, string matKhauMoi);
        Task<bool> ValidateNguoiDungDataAsync(NguoiDung nguoiDung);
        Task<string> MaHoaMatKhauAsync(string matKhau);
        Task<bool> KiemTraMatKhauAsync(string matKhau, string matKhauDaMaHoa);
    }

}