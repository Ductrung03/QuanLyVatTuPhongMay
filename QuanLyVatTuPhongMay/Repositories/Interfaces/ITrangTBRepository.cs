using QuanLyVatTuPhongMay.Models;

namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface ITrangTBRepository : IBaseRepository<TrangTB>
    {
        Task<IEnumerable<TrangTB>> GetByPhongMayAsync(int maPhongMay);
        Task<IEnumerable<TrangTB>> GetByLoaiAsync(int maLoai);
        Task<IEnumerable<TrangTB>> GetByThuongHieuAsync(int maThuongHieu);
        Task<IEnumerable<TrangTB>> GetByTinhTrangAsync(string tinhTrang);
        Task<IEnumerable<TrangTB>> GetCanBaoTriAsync();
        Task<bool> CapNhatSoLanSuaAsync(int maTTB);
        Task<IEnumerable<TrangTB>> TimKiemAsync(string? tuKhoa, int? maPhongMay, int? maLoai,
            int? maThuongHieu, string? tinhTrang, DateTime? tuNgay, DateTime? denNgay,
            int? giaMin, int? giaMax);
        Task<object> GetBaoCaoTheoPhongAsync();
        Task<object> GetThongKeTheoThuongHieuAsync();
    }
}
