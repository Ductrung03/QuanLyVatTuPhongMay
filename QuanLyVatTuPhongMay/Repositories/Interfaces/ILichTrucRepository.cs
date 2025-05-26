using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface ILichTrucRepository : IBaseRepository<LichTruc>
    {
        Task<IEnumerable<LichTruc>> GetByNgayAsync(DateTime ngay);
        Task<IEnumerable<LichTruc>> GetByPhongMayAsync(int maPhongMay);
        Task<IEnumerable<LichTruc>> GetByNhanVienAsync(int maNV);
    }
}