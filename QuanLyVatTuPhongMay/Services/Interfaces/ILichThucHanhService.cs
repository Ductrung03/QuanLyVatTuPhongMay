using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface ILichThucHanhService : IBaseService<LichThucHanh>
    {
        Task<IEnumerable<LichThucHanh>> GetByNguoiDungAsync(int maNguoiDung);
        Task<IEnumerable<LichThucHanh>> GetByTrangThaiAsync(string trangThai);
        Task<bool> DatLichAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT, int maNguoiDung);
        Task<bool> HuyLichAsync(int maLich, int maNguoiDung);
        Task<bool> CapNhatTrangThaiAsync(int maLich, string trangThai);
        Task<IEnumerable<LichThucHanh>> GetLichTrongKhoangAsync(DateTime tuNgay, DateTime denNgay);
        Task<bool> ValidateLichThucHanhAsync(LichThucHanh lichThucHanh);
        Task<IEnumerable<LichThucHanh>> GetLichSapToiAsync(int maNguoiDung);
        Task<bool> KiemTraTrungLichAsync(DateTime thoiGianBD, DateTime thoiGianKT, int? maLichBỏQua = null);
    }
}