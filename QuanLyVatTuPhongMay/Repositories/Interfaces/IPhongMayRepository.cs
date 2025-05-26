using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface IPhongMayRepository : IBaseRepository<PhongMay>
    {
        Task<bool> KiemTraPhongKhaDungAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT);
        Task<IEnumerable<object>> GetBaoCaoSuDungAsync(DateTime tuNgay, DateTime denNgay, int? maPhongMay = null);
    }
}