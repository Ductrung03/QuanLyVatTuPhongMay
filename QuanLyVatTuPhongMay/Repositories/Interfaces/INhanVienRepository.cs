using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface INhanVienRepository : IBaseRepository<NhanVien>
    {
        Task<IEnumerable<NhanVien>> TimKiemTheoTenAsync(string ten);
    }
}