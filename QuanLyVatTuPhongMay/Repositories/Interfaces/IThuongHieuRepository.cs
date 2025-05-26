using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface IThuongHieuRepository : IBaseRepository<ThuongHieu>
    {
        Task<bool> KiemTraTenTonTaiAsync(string tenThuongHieu);
    }
}