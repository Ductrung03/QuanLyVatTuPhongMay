using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using System.Data;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class TrangTBRepository : BaseRepository<TrangTB>, ITrangTBRepository
    {
        public TrangTBRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TrangTB>> GetAllAsync()
        {
            return await _context.TrangTBs
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .Include(t => t.LichThucHanh)
                .ToListAsync();
        }

        public override async Task<TrangTB?> GetByIdAsync(int id)
        {
            return await _context.TrangTBs
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .Include(t => t.LichThucHanh)
                .FirstOrDefaultAsync(t => t.MaTTB == id);
        }

        public override async Task<TrangTB> CreateAsync(TrangTB entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaPhongMay", entity.MaPhongMay ?? (object)DBNull.Value),
                new SqlParameter("@MaLoai", entity.MaLoai ?? (object)DBNull.Value),
                new SqlParameter("@GiaTien", entity.GiaTien ?? (object)DBNull.Value),
                new SqlParameter("@TinhTrang", entity.TinhTrang ?? (object)DBNull.Value),
                new SqlParameter("@NgayNhap", entity.NgayNhap),
                new SqlParameter("@SoLanSua", entity.SoLanSua ?? 0),
                new SqlParameter("@MaThuongHieu", entity.MaThuongHieu ?? (object)DBNull.Value),
                new SqlParameter("@MaLich", entity.MaLich ?? (object)DBNull.Value)
            };

            var result = await _context.Database.SqlQueryRaw<int>(
                "EXEC sp_ThemThietBi @MaPhongMay, @MaLoai, @GiaTien, @TinhTrang, @NgayNhap, @SoLanSua, @MaThuongHieu, @MaLich",
                parameters).FirstOrDefaultAsync();

            entity.MaTTB = result;
            return entity;
        }

        public override async Task<TrangTB> UpdateAsync(TrangTB entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaTTB", entity.MaTTB),
                new SqlParameter("@MaPhongMay", entity.MaPhongMay ?? (object)DBNull.Value),
                new SqlParameter("@MaLoai", entity.MaLoai ?? (object)DBNull.Value),
                new SqlParameter("@GiaTien", entity.GiaTien ?? (object)DBNull.Value),
                new SqlParameter("@TinhTrang", entity.TinhTrang ?? (object)DBNull.Value),
                new SqlParameter("@NgayNhap", entity.NgayNhap),
                new SqlParameter("@SoLanSua", entity.SoLanSua ?? 0),
                new SqlParameter("@MaThuongHieu", entity.MaThuongHieu ?? (object)DBNull.Value),
                new SqlParameter("@MaLich", entity.MaLich ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SuaThietBi @MaTTB, @MaPhongMay, @MaLoai, @GiaTien, @TinhTrang, @NgayNhap, @SoLanSua, @MaThuongHieu, @MaLich",
                parameters);

            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaTTB", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaThietBi @MaTTB", parameter);
            return true;
        }

        public async Task<IEnumerable<TrangTB>> GetByPhongMayAsync(int maPhongMay)
        {
            var parameter = new SqlParameter("@MaPhongMay", maPhongMay);
            return await _context.TrangTBs
                .FromSqlRaw("EXEC sp_LayThietBi NULL, @MaPhongMay", parameter)
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrangTB>> GetByLoaiAsync(int maLoai)
        {
            return await _context.TrangTBs
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .Where(t => t.MaLoai == maLoai)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrangTB>> GetByThuongHieuAsync(int maThuongHieu)
        {
            return await _context.TrangTBs
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .Where(t => t.MaThuongHieu == maThuongHieu)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrangTB>> GetByTinhTrangAsync(string tinhTrang)
        {
            return await _context.TrangTBs
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .Where(t => t.TinhTrang != null && t.TinhTrang.Contains(tinhTrang))
                .ToListAsync();
        }

        public async Task<IEnumerable<TrangTB>> GetCanBaoTriAsync()
        {
            var parameter = new SqlParameter("@SoLanSuaToiDa", 5);
            return await _context.TrangTBs
                .FromSqlRaw(@"
                    SELECT t.*, p.TenPhongMay, l.TenLoai, th.TenThuongHieu
                    FROM TrangTB t
                    JOIN PhongMay p ON t.MaPhongMay = p.MaPhongMay
                    JOIN Loai l ON t.MaLoai = l.MaLoai
                    JOIN ThuongHieu th ON t.MaThuongHieu = th.MaThuongHieu
                    WHERE t.SoLanSua >= @SoLanSuaToiDa 
                       OR t.TinhTrang LIKE N'%hỏng%' 
                       OR t.TinhTrang LIKE N'%cần sửa%'", parameter)
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .ToListAsync();
        }

        public async Task<bool> CapNhatSoLanSuaAsync(int maTTB)
        {
            var parameter = new SqlParameter("@MaTTB", maTTB);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_CapNhatSoLanSua @MaTTB", parameter);
            return true;
        }

        public async Task<IEnumerable<TrangTB>> TimKiemAsync(string? tuKhoa, int? maPhongMay, int? maLoai,
            int? maThuongHieu, string? tinhTrang, DateTime? tuNgay, DateTime? denNgay,
            int? giaMin, int? giaMax)
        {
            var parameters = new[]
            {
                new SqlParameter("@TuKhoa", tuKhoa ?? (object)DBNull.Value),
                new SqlParameter("@MaPhongMay", maPhongMay ?? (object)DBNull.Value),
                new SqlParameter("@MaLoai", maLoai ?? (object)DBNull.Value),
                new SqlParameter("@MaThuongHieu", maThuongHieu ?? (object)DBNull.Value),
                new SqlParameter("@TinhTrang", tinhTrang ?? (object)DBNull.Value),
                new SqlParameter("@TuNgay", tuNgay ?? (object)DBNull.Value),
                new SqlParameter("@DenNgay", denNgay ?? (object)DBNull.Value),
                new SqlParameter("@GiaMin", giaMin ?? (object)DBNull.Value),
                new SqlParameter("@GiaMax", giaMax ?? (object)DBNull.Value)
            };

            return await _context.TrangTBs
                .FromSqlRaw("EXEC sp_TimKiemThietBi @TuKhoa, @MaPhongMay, @MaLoai, @MaThuongHieu, @TinhTrang, @TuNgay, @DenNgay, @GiaMin, @GiaMax", parameters)
                .Include(t => t.PhongMay)
                .Include(t => t.Loai)
                .Include(t => t.ThuongHieu)
                .ToListAsync();
        }

        public async Task<object> GetBaoCaoTheoPhongAsync()
        {
            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_BaoCaoThietBiTheoPhong";
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    TenPhongMay = reader.GetString("TenPhongMay"),
                    TenLoai = reader.GetString("TenLoai"),
                    TenThuongHieu = reader.GetString("TenThuongHieu"),
                    SoLuong = reader.GetInt32("SoLuong"),
                    TongGiaTri = reader.GetInt32("TongGiaTri"),
                    TrungBinhSoLanSua = reader.GetDecimal("TrungBinhSoLanSua")
                });
            }

            await connection.CloseAsync();
            return results;
        }

        public async Task<object> GetThongKeTheoThuongHieuAsync()
        {
            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_ThongKeThietBiTheoThuongHieu";
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    TenThuongHieu = reader.GetString("TenThuongHieu"),
                    SoLuongThietBi = reader.GetInt32("SoLuongThietBi"),
                    TongGiaTri = reader.GetInt32("TongGiaTri"),
                    GiaTriTrungBinh = reader.GetInt32("GiaTriTrungBinh"),
                    ThietBiTot = reader.GetInt32("ThietBiTot"),
                    ThietBiCanSua = reader.GetInt32("ThietBiCanSua"),
                    TrungBinhSoLanSua = reader.GetDouble("TrungBinhSoLanSua")
                });
            }

            await connection.CloseAsync();
            return results;
        }
    }
}