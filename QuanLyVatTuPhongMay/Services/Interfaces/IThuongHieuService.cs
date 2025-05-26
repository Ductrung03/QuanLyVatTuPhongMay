using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface IThuongHieuService : IBaseService<ThuongHieu>
    {
        Task<bool> KiemTraTenTonTaiAsync(string tenThuongHieu);
        Task<bool> ValidateThuongHieuDataAsync(ThuongHieu thuongHieu);
        Task<IEnumerable<TrangTB>> GetThietBiTheoThuongHieuAsync(int maThuongHieu);
    }
} 