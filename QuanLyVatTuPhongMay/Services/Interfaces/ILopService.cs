using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface ILopService : IBaseService<Lop>
    {
        Task<bool> KiemTraTenTonTaiAsync(string tenLop);
        Task<bool> ValidateLopDataAsync(Lop lop);
    }
} 