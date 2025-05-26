using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface ILichThucHanhRepository : IBaseRepository<LichThucHanh>
    {
        Task<IEnumerable<LichThucHanh>> GetByNguoiDungAsync(int maNguoiDung);
        Task<IEnumerable<LichThucHanh>> GetByTrangThaiAsync(string trangThai);
        Task<bool> DatLichAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT,
            int maNguoiDung, string trangThai = "Đã đặt");
        Task<bool> HuyLichAsync(int maLich, int maNguoiDung);
        Task<IEnumerable<LichThucHanh>> GetLichTrongKhoangAsync(DateTime tuNgay, DateTime denNgay);
    }
}