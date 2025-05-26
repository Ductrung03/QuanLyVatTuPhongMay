using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface ILichTrucService : IBaseService<LichTruc>
    {
        Task<IEnumerable<LichTruc>> GetByNgayAsync(DateTime ngay);
        Task<IEnumerable<LichTruc>> GetByPhongMayAsync(int maPhongMay);
        Task<IEnumerable<LichTruc>> GetByNhanVienAsync(int maNV);
        Task<bool> ValidateLichTrucDataAsync(LichTruc lichTruc);
        Task<bool> KiemTraTrungLichTrucAsync(int maNV, DateTime ngay, int? maLichBỏQua = null);
    }
} 