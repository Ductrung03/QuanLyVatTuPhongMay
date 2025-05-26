using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface ILoaiService : IBaseService<Loai>
    {
        Task<bool> KiemTraTenTonTaiAsync(string tenLoai);
        Task<bool> ValidateLoaiDataAsync(Loai loai);
        Task<IEnumerable<TrangTB>> GetThietBiTheoLoaiAsync(int maLoai);
    }
} 