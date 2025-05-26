using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface INhatKyThayDoiService
    {
        Task<bool> ThemNhatKyAsync(int maLichSu, string noiDung, string? thongTinCu, string? thongTinMoi);
        Task<IEnumerable<NhatKyThayDoi>> GetByLichSuAsync(int maLichSu);
        Task<IEnumerable<NhatKyThayDoi>> GetTrongKhoangAsync(DateTime tuNgay, DateTime denNgay);
        Task<IEnumerable<NhatKyThayDoi>> GetTatCaNhatKyAsync();
    }
} 