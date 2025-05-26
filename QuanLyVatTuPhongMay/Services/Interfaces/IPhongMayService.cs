using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface IPhongMayService : IBaseService<PhongMay>
    {
        Task<bool> KiemTraPhongKhaDungAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT);
        Task<IEnumerable<object>> GetBaoCaoSuDungAsync(DateTime tuNgay, DateTime denNgay, int? maPhongMay = null);
        Task<bool> ValidatePhongMayDataAsync(PhongMay phongMay);
        Task<IEnumerable<TrangTB>> GetThietBiTrongPhongAsync(int maPhongMay);
    }
} 