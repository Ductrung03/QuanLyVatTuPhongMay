using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface ITrangTBService : IBaseService<TrangTB>
    {
        Task<IEnumerable<TrangTB>> GetByPhongMayAsync(int maPhongMay);
        Task<IEnumerable<TrangTB>> GetByLoaiAsync(int maLoai);
        Task<IEnumerable<TrangTB>> GetByThuongHieuAsync(int maThuongHieu);
        Task<IEnumerable<TrangTB>> GetByTinhTrangAsync(string tinhTrang);
        Task<IEnumerable<TrangTB>> GetCanBaoTriAsync();
        Task<bool> CapNhatSoLanSuaAsync(int maTTB);
        Task<bool> ChuyenTrangThaiThietBiAsync(int maTTB, string tinhTrangMoi, int maNguoiDung, string? ghiChu = null);
        Task<IEnumerable<TrangTB>> TimKiemNangCaoAsync(string? tuKhoa, int? maPhongMay, int? maLoai,
            int? maThuongHieu, string? tinhTrang, DateTime? tuNgay, DateTime? denNgay,
            int? giaMin, int? giaMax);
        Task<object> GetBaoCaoTheoPhongAsync();
        Task<object> GetThongKeTheoThuongHieuAsync();
        Task<object> GetLichSuHoatDongAsync(int maTTB);
        Task<IEnumerable<object>> GetCanhBaoThietBiAsync();
        Task<bool> ValidateThietBiDataAsync(TrangTB thietBi);
    }
} 