using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface ILopRepository : IBaseRepository<Lop>
    {
        Task<bool> KiemTraTenTonTaiAsync(string tenLop);
    }
}