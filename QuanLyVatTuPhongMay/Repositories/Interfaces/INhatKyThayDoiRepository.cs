using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface INhatKyThayDoiRepository : IBaseRepository<NhatKyThayDoi>
    {
        Task<bool> ThemNhatKyAsync(int maLichSu, string noiDung, string? thongTinCu, string? thongTinMoi);
        Task<IEnumerable<NhatKyThayDoi>> GetByLichSuAsync(int maLichSu);
        Task<IEnumerable<NhatKyThayDoi>> GetTrongKhoangAsync(DateTime tuNgay, DateTime denNgay);
    }

}