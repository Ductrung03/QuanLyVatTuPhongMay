using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface INhanVienService : IBaseService<NhanVien>
    {
        Task<IEnumerable<NhanVien>> TimKiemTheoTenAsync(string ten);
        Task<bool> ValidateNhanVienDataAsync(NhanVien nhanVien);
        Task<IEnumerable<LichTruc>> GetLichTrucNhanVienAsync(int maNV);
    }
} 