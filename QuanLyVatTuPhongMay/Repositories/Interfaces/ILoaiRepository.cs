using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface ILoaiRepository : IBaseRepository<Loai>
    {
        Task<bool> KiemTraTenTonTaiAsync(string tenLoai);
    }
}